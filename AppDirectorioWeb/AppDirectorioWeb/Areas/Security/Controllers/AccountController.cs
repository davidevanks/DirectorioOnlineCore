using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<IdentityUser> _signInManager;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        public IActionResult GetMyProfile(string userId)
        {
            return View();
        }

        public IActionResult Logout(string returnUrl = null)
        {
             _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }
        #endregion Public Methods
    }
}