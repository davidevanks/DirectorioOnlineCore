using APISeguridadWEB.ExtraServices.EmailService;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.Models.Identity.AccountViewModels;
using Models.Models.Identity.ManageViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APISeguridadWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly RoleManager<DapperIdentityRole> _roleManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private ResponseViewModel _response = new ResponseViewModel();
        
        private DapperIdentityUser _user = new DapperIdentityUser();

        #endregion Private Fields

        #region Public Constructors

        public AccountController(
            UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            RoleManager<DapperIdentityRole> roleManager,
            IEmailService emailService,
            IConfiguration configuration

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [Route("api/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _response.MessageResponse = "La contraseña se cambio correctamente";
                    _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                }
                else
                {
                    _response.MessageResponse = "Error al cambiar contraseña";
                    _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                }
            }
            else
            {
                _response.MessageResponse = "Invalid Information!";
                _response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            return Ok(_response);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [Route("api/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                _response.MessageResponse = "Usuario/codigo inválidos!";
                _response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            else
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _response.MessageResponse = "Usuario no existe!";
                    _response.MessageResponseCode = ResponseViewModel.MessageCode.UserNotExist;
                }
                else
                {
                    _response.IdentityResult = await _userManager.ConfirmEmailAsync(user, code);
                    if (_response.IdentityResult.Succeeded)
                    {
                        _response.MessageResponse = "El correo se confirmó con éxito, puede hacer login";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.EmailConfirmedSuccess;
                    }
                    else
                    {
                        _response.MessageResponse = "El correo no se confirmó";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
            }

            return Ok(_response);
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [Route("api/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UrlContext == "") model.UrlContext = HttpContext.Request.Scheme;

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    _response.MessageResponse = "Invalid Information!";
                    _response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
                }
                else
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userEmail = user.Email, code = code }, protocol: model.UrlContext);
                    var resulemail = _emailService.SendResetPasswordEmail(model.Email, "Restablecer contraseña - Listy", model,
                        callbackUrl);

                    if (resulemail.MessageResponseCode == ResponseViewModel.MessageCode.Success)
                    {
                        _response.MessageResponse = "Se envio con exito un correo para restablecer la contraseña";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                    }
                    else
                    {
                        _response.MessageResponse = "Error en envio de correo de restablecer contraseña";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
            }

            return Ok(_response);
        }

        /// <summary>
        /// Metodo para la hacer login desde formulario login normal.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (ModelState.IsValid)
                {
                    _user = await _userManager.FindByEmailAsync(model.Email);
                    if (_user != null)
                    {
                        var checkPassword = await _signInManager.CheckPasswordSignInAsync(_user, model.Password, false);
                        if (checkPassword.IsNotAllowed)
                        {
                            _response.SignInResult = checkPassword;
                            _response.MessageResponse = "Email no confirmado aún!";
                            _response.MessageResponseCode = ResponseViewModel.MessageCode.EmailNotConfirmed;
                        }
                        else if (checkPassword.Succeeded)
                        {
                            var roles= await _userManager.GetRolesAsync(_user);
                            _user.RolesNames = roles.ToList();
                            _response.Token = BuildToken(_user);
                            _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                            _response.MessageResponse = "Login éxitoso!";
                            _response.UserInfo = _user;
                        }
                        else
                        {
                            _response.SignInResult = checkPassword;
                            _response.MessageResponse = "Password Incorrecto!";
                            _response.MessageResponseCode = ResponseViewModel.MessageCode.IncorrectPassword;
                        }
                    }
                    else
                    {
                        _response.MessageResponse = "Usuario no esta registrado!";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.UserNotExist;
                    }
                }
            }
            catch (Exception e)
            {
                _response.MessageResponse = e.Message;
                _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }

            // por defecto mientras tanto , el lockout de password esta deshabilitado, desarrollar lógica posterior.

            return Ok(_response);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [Route("api/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _response.MessageResponse = "Log Off exitoso!";
            _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
            return Ok(_response);
        }

        /// <summary>
        /// Metodo para la creación de un usuario por primera vez, metodo normal, sin usar proveedores de externos. Se manda nombre de rol a asignar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (model.UrlContext == "") model.UrlContext = HttpContext.Request.Scheme;

            IdentityResult resultRol = null;
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = new DapperIdentityUser
                {
                    FirstName = model.FirstName,
                    UserName = model.Email,
                    Email = model.Email,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    AllowMarketing = model.AllowMarketing,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    AcceptTerms = model.AcceptTerms,
                    Active = true,
                    DateCreate = DateTime.Now
                };
                var resultCreate = await _userManager.CreateAsync(user, model.Password);

                //VERIFICAMOS SI SE CREO USUARIO PARA ASIGNAR ROL (el rol es el plan de membresia)
                if (resultCreate.Succeeded)
                {
                    resultRol = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (resultRol.Succeeded)
                    {
                        //logica para confirmar cuenta

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = token }, protocol: model.UrlContext);

                        //falta implementar ennvio de correo con link
                        var resulemail = _emailService.SendAccountConfirmationEmail(model.Email, "Confirmación de cuenta - Listy", model, callbackUrl);

                        if (resulemail.MessageResponseCode == ResponseViewModel.MessageCode.Success)
                        {
                            _response.IdentityResult = resultRol;
                            _response.MessageResponse = "Registro éxitoso, se le envio un correo con el link para confirmar cuenta";
                            _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                        }
                        else
                        {
                            _response.IdentityResult = resultRol;
                            _response.MessageResponse = "Registro éxitoso, pero hubo un error en el envio de correo";
                            _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                        }
                    }
                    else
                    {
                        _response.IdentityResult = resultRol;
                        _response.MessageResponse = "Usuario creado con éxito, pero no se logro asignar rol";
                        _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
                else
                {
                    _response.IdentityResult = resultCreate;
                    _response.MessageResponse = "Error al crear usuario!";
                    _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                }
            }
            catch (Exception e)
            {
                _response.MessageResponse = e.Message;
                _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }

            return Ok(_response);
        }

        [HttpPost]
        [Route("api/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                _response.MessageResponse = "Invalid Information!";
                _response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                _response.MessageResponse = "La contraseña se cambio correctamente, intente hacer login usando la contraseña nueva";
                _response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
            }
            else
            {
                _response.MessageResponse = "Error al cambiar la contraseña";
                _response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }
            return Ok(_response);
        }

        private TokenViewModel BuildToken(DapperIdentityUser userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName),
                new Claim("Rol", userInfo.Roles.FirstOrDefault()?.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["LlaveToken"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMonths(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: expiration,
                signingCredentials: creds);


            var claimsIdentity =
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Id", userInfo.UserName),
                        new Claim("UserName", userInfo.UserName),
                        new Claim("Rol", userInfo.Roles.FirstOrDefault()?.RoleId.ToString()),
                    }
                );
            HttpContext.User = new ClaimsPrincipal(new[] { claimsIdentity });

            return new TokenViewModel { Token = new JwtSecurityTokenHandler().WriteToken(token), ExpirationDate = expiration };
            
        }

        #endregion Public Constructors
    }
}