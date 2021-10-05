using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Identity.AccountViewModels;
using System;
using System.Threading.Tasks;
using APISeguridadWEB.ExtraServices.EmailService;
using Microsoft.AspNetCore.Hosting;
using Models.Models.Identity.ManageViewModels;

namespace APISeguridadWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly RoleManager<DapperIdentityRole> _roleManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly UserManager<DapperIdentityUser> _userManager;
      
        private readonly IEmailService _emailService;
        private ResponseViewModel response = new ResponseViewModel();

        private DapperIdentityUser user = new DapperIdentityUser();

        #endregion Private Fields

        #region Public Constructors

        public AccountController(
            UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            RoleManager<DapperIdentityRole> roleManager,
            IEmailService emailService
           
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
           
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [Route("api/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            
            if (userId == null || code == null)
            {
                response.MessageResponse = "Usuario/codigo inválidos!";
                response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            else
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.MessageResponse = "Usuario no existe!";
                    response.MessageResponseCode = ResponseViewModel.MessageCode.UserNotExist;
                }
                else
                {
                    response.IdentityResult = await _userManager.ConfirmEmailAsync(user, code);
                    if (response.IdentityResult.Succeeded)
                    {
                        response.MessageResponse = "El correo se confirmó con éxito, puede hacer login";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.EmailConfirmedSuccess;
                    }
                    else
                    {
                        response.MessageResponse = "El correo no se confirmó";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
            }

            return Ok(response);
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
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (checkPassword.IsNotAllowed)
                        {
                            response.SignInResult = checkPassword;
                            response.MessageResponse = "Email no confirmado aún!";
                            response.MessageResponseCode = ResponseViewModel.MessageCode.EmailNotConfirmed;
                        }
                        else if (checkPassword.Succeeded)
                        {
                            response.SignInResult = checkPassword;
                            response.MessageResponse = "Login exitoso!";
                            response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                        }
                        else
                        {
                            response.SignInResult = checkPassword;
                            response.MessageResponse = "Password Incorrecto!";
                            response.MessageResponseCode = ResponseViewModel.MessageCode.IncorrectPassword;
                        }
                    }
                    else
                    {
                        response.MessageResponse = "Usuario no esta registrado!";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.UserNotExist;
                    }
                }
            }
            catch (Exception e)
            {
                response.MessageResponse = e.Message;
                response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }

            // por defecto mientras tanto , el lockout de password esta deshabilitado, desarrollar lógica posterior.

            return Ok(response);
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
                            response.IdentityResult = resultRol;
                            response.MessageResponse = "Registro éxitoso, se le envio un correo con el link para confirmar cuenta";
                            response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                        }
                        else
                        {
                            response.IdentityResult = resultRol;
                            response.MessageResponse = "Registro éxitoso, pero hubo un error en el envio de correo";
                            response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                        }

                    }
                    else
                    {
                        response.IdentityResult = resultRol;
                        response.MessageResponse = "Usuario creado con éxito, pero no se logro asignar rol";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
                else
                {
                    response.IdentityResult = resultCreate;
                    response.MessageResponse = "Error al crear usuario!";
                    response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                }
            }
            catch (Exception e)
            {
                response.MessageResponse = e.Message;
                response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }

            return Ok(response);
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
                    response.MessageResponse = "Invalid Information!";
                    response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
                }
                else
                {
                    
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userEmail = user.Email, code = code }, protocol: model.UrlContext);
                    var resulemail=   _emailService.SendResetPasswordEmail(model.Email, "Restablecer contraseña - Listy",model,
                        callbackUrl);

                    if (resulemail.MessageResponseCode == ResponseViewModel.MessageCode.Success)
                    {
                        response.MessageResponse = "Se envio con exito un correo para restablecer la contraseña";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                    }
                    else
                    {
                        response.MessageResponse = "Error en envio de correo de restablecer contraseña";
                        response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                    }
                }
                
            }

          
            return Ok(response);
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
              
                response.MessageResponse = "Invalid Information!";
                response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                response.MessageResponse = "La contraseña se cambio correctamente, intente hacer login usando la contraseña nueva";
                response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
            }
            else
            {
                response.MessageResponse = "Error al cambiar la contraseña";
                response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
            }
            return Ok(response);
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
                    response.MessageResponse = "La contraseña se cambio correctamente";
                    response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
                }
                else
                {
                    response.MessageResponse = "Error al cambiar contraseña";
                    response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                }
              
            }
            else
            {
                response.MessageResponse = "Invalid Information!";
                response.MessageResponseCode = ResponseViewModel.MessageCode.InvalidInformation;
            }
            return Ok(response);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [Route("api/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            response.MessageResponse = "Log Off exitoso!";
            response.MessageResponseCode = ResponseViewModel.MessageCode.Success;
            return Ok(response);
        }
        #endregion Public Constructors
    }
}