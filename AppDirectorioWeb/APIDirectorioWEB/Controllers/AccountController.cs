using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Identity.AccountViewModels;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace APISeguridadWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly RoleManager<DapperIdentityRole> _roleManager;
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
            var result = new SignInResult();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ModelState.IsValid)
            {
                // por defecto mientras tanto , el lockout de password esta deshabilitado, desarrollar lógica posterior.
               result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                
            }
           

            return Ok(result);

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
            
            var user = new DapperIdentityUser {FirstName = model.FirstName, UserName = model.Email,
                Email = model.Email,LastName = model.LastName,PhoneNumber = model.PhoneNumber
                ,AllowMarketing = model.AllowMarketing,TwoFactorEnabled = model.TwoFactorEnabled};
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