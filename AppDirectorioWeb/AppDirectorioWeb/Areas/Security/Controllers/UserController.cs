using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Utiles;

namespace AppDirectorioWeb.Controllers
{
    [Area("Security")]
    [Authorize(Roles = SP.Role_Admin)]
    public class UserController : Controller
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion Private Fields

        #region Public Constructors

        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DirectorioOnlineCoreContext db)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }

        #endregion Public Methods

        #region API_CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            userList = _unitOfWork.UserDetail.GetAUsersDetails("");
            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            string message = "";
            if (user == null)
            {
                return Json(new { success = false, message = "Usuario no existe!" });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock
                user.LockoutEnd = DateTime.Now;
                message = " Desbloqueado!";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                message = " Bloqueado!";
            }

            _db.SaveChanges();
            return Json(new { success = true, message = "Usuario" + message });
        }

        #endregion API_CALLS
    }
}