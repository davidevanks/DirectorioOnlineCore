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

namespace AppDirectorioWeb.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public AccountController( SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        public IActionResult GetMyProfile(string userId)
        {
            UserViewModel userProfile = _unitOfWork.UserDetail.GetAUsersDetails(userId).FirstOrDefault();
            return View(userProfile);
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