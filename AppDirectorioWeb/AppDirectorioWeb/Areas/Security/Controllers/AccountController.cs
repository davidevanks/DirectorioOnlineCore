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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AppDirectorioWeb.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion Private Fields

        #region Public Constructors

        public AccountController( SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        public IActionResult GetMyProfile(string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            
            UserViewModel userProfile = _unitOfWork.UserDetail.GetAUsersDetails(user.Result.Id).FirstOrDefault();
            return View(userProfile);
        }

        [Authorize]
        public IActionResult UpdateMyProfile(UserViewModel userProfile)
        {
            userProfile.UpdateUser = HttpContext.Session.GetString("UserId");
            userProfile.UpdateDate = DateTime.Now;
            userProfile.UserName =userProfile.Email;
            _unitOfWork.UserDetail.Update(userProfile);
            _unitOfWork.Save();
            UserViewModel userProfileUpdated = _unitOfWork.UserDetail.GetAUsersDetails(userProfile.Id).FirstOrDefault();
            ViewBag.Updated = "1";
            return View(nameof(GetMyProfile), userProfileUpdated);
        }


        [Authorize]
        public IActionResult ChangePassword(UserViewModel userProfile)
        {
            UserViewModel userProfileUpdated = _unitOfWork.UserDetail.GetAUsersDetails(userProfile.Id).FirstOrDefault();
            var user = _userManager.FindByNameAsync(userProfile.UserName).Result;
            var changePasswordResult =  _userManager.ChangePasswordAsync(user, userProfile.ChangePassword.OldPassword, userProfile.ChangePassword.NewPassword).Result;
            if (!changePasswordResult.Succeeded)
            {
                return View(nameof(GetMyProfile), userProfileUpdated);
            }

             _signInManager.RefreshSignInAsync(user);
           
            ViewBag.UpdatedPass = "1";
          
            return View(nameof(GetMyProfile), userProfileUpdated);
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