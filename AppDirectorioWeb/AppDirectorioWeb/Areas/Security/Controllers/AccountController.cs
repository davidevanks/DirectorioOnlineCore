﻿using DataAccess.Repository.IRepository;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        public IActionResult ChangePassword(UserViewModel userProfile)
        {
            UserViewModel userProfileUpdated = _unitOfWork.UserDetail.GetAUsersDetails(userProfile.Id).FirstOrDefault();
            var user = _userManager.FindByNameAsync(userProfile.UserName).Result;
            var changePasswordResult = _userManager.ChangePasswordAsync(user, userProfile.ChangePassword.OldPassword, userProfile.ChangePassword.NewPassword).Result;
            if (!changePasswordResult.Succeeded)
            {
                return View(nameof(GetMyProfile), userProfileUpdated);
            }

            _signInManager.RefreshSignInAsync(user);

            ViewBag.UpdatedPass = "1";

            return View(nameof(GetMyProfile), userProfileUpdated);
        }

        [Authorize]
        public IActionResult GetMyProfile(string userName)
        {
            var user = _userManager.FindByNameAsync(userName);

            UserViewModel userProfile = _unitOfWork.UserDetail.GetAUsersDetails(user.Result.Id).FirstOrDefault();
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

        [Authorize]
        public async Task<IActionResult> UpdateMyPictureProfile(UserViewModel userProfile)
        {
            string uniqueFileName = "";
            if (userProfile.Picture != null)
            {
                
                uniqueFileName = Guid.NewGuid().ToString() + "_picprofile_" + userProfile.Picture.FileName;
                //--------firebaselogic
                Stream stream = userProfile.Picture.OpenReadStream();
                //firebase logic to upload file
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseSetting.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseSetting.AuthEmail, FirebaseSetting.AuthPassword);


                //cancellation token
                var cancellation = new CancellationTokenSource();

                var upload = new FirebaseStorage(
                    FirebaseSetting.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true

                    }
                    ).Child("profilePicture")
                    .Child($"{uniqueFileName}")
                    .PutAsync(stream, cancellation.Token);



                uniqueFileName = await upload;

                //--------------------
            }




            userProfile.ProfilePicture = uniqueFileName;
            _unitOfWork.UserDetail.UpdateProfilePicture(userProfile);
            _unitOfWork.Save();
            var user = _userManager.FindByNameAsync(userProfile.UserName).Result;
            await _signInManager.RefreshSignInAsync(user);
            UserViewModel userProfileUpdated = _unitOfWork.UserDetail.GetAUsersDetails(userProfile.Id).FirstOrDefault();

            return View(nameof(GetMyProfile), userProfileUpdated);
        }

        [Authorize]
        public IActionResult UpdateMyProfile(UserViewModel userProfile)
        {
            userProfile.UpdateUser = HttpContext.Session.GetString("UserId");
            userProfile.UpdateDate = DateTime.Now;
            userProfile.UserName = userProfile.Email;
            _unitOfWork.UserDetail.Update(userProfile);
            _unitOfWork.Save();
            UserViewModel userProfileUpdated = _unitOfWork.UserDetail.GetAUsersDetails(userProfile.Id).FirstOrDefault();
            ViewBag.Updated = "1";
            return View(nameof(GetMyProfile), userProfileUpdated);
        }

        #endregion Public Methods
    }
}