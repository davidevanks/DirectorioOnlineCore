using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Identity.AccountViewModels;
using System.Threading.Tasks;

namespace APIDirectorioWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly UserManager<DapperIdentityUser> _userManager;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(
            UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            //Metodo inicial, solo es prueba, despues hay que completar lógica.
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new DapperIdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
            }

            return Created("result", result);
        }

        #endregion Public Constructors
    }
}