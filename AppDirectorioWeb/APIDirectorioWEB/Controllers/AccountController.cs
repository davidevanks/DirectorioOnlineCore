using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Identity.AccountViewModels;
using System;
using System.Threading.Tasks;

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
        private ResponseViewModel response = new ResponseViewModel();
        private DapperIdentityUser user = new DapperIdentityUser();

        #endregion Private Fields

        #region Public Constructors

        public AccountController(
            UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            RoleManager<DapperIdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                            response.MessageResponseCode= ResponseViewModel.MessageCode.IncorrectPassword;
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
                throw;
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
            IdentityResult result = null;
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new DapperIdentityUser
            {
                FirstName = model.FirstName,
                UserName = model.Email,
                Email = model.Email,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
                ,
                AllowMarketing = model.AllowMarketing,
                TwoFactorEnabled = model.TwoFactorEnabled
            };
            var resultCreate = await _userManager.CreateAsync(user, model.Password);

            //VERIFICAMOS SI SE CREO USUARIO PARA ASIGNAR ROL (el rol es el plan de membresia)
            if (resultCreate.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, model.RoleName);

                //validamos el tipo de rol(membresia a asignar, ya que el consumidor )
            }
            else
            {
                result = resultCreate;
            }

            return Ok(result);
        }

        #endregion Public Constructors
    }
}