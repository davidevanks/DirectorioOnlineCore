using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Cuponera.Controllers
{
    [Area("Cuponera")]
    public class CuponeraController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public CuponeraController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)  
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult Index()
        {
          
                return View();
        }

        [Authorize(Roles = SP.Role_BusinesAdmin)]
        public IActionResult Add()
        {
            CuponeraViewModel model = new CuponeraViewModel();
            model.Id = 0;
            return View(model);
        }


        #region Api
        [HttpGet]
        public IActionResult GetCupons()
        {
            string userId = "";
            //verificar que tipo de usuario esta consultabdo esto
            //hacer logica para traer los cupones, lgica para bussinesAdmin y Admin site
            if (User.IsInRole(SP.Role_BusinesAdmin))
            {
                userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            }

            var cupons = _unitOfWork.Cuponera.GetCupons(userId);

            return Json(new { data = cupons });
        }
        #endregion
    }
}
