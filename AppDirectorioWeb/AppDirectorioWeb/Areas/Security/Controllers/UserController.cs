using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using Utiles;

namespace AppDirectorioWeb.Controllers
{
    [Area("Catalogos")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DirectorioOnlineCoreContext _db;
        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DirectorioOnlineCoreContext db)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }




        #region API_CALLS

        [HttpGet]
         public IActionResult GetAll()
         {
             var user = _userManager.Users.ToList();
             var userRol = _db.UserRoles.ToList();
             var roles = _roleManager.Roles.ToList();

             List<UserViewModel> userList = new List<UserViewModel>();
             foreach (var u in user)
             {
                 UserViewModel userAdd = new UserViewModel();
                 var roleId = userRol.FirstOrDefault(x => x.UserId == u.Id).RoleId;
                 userAdd.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;
                 userAdd.Email = u.Email;
                 userAdd.Id = u.Id;

                 userList.Add(userAdd);

             }


             return Json(new{data= userList });
         }


       
         #endregion
    }
}
