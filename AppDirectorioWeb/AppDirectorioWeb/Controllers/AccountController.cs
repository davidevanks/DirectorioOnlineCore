using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AppDirectorioWeb.RequestProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Models.Models.Identity.AccountViewModels;

namespace AppDirectorioWeb.Controllers
{
    [Authorize]
   
    public class AccountController : Controller
    {

        private readonly IBackendHelper _backendHelper;
       
        public AccountController(IBackendHelper backendHelper)
        {
            _backendHelper = backendHelper;
    
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                
               
                var response = await _backendHelper.PostAsync<ResponseViewModel>("/api/Account/api/Login", model);
                
                if (response.MessageResponseCode == ResponseViewModel.MessageCode.Success && !String.IsNullOrEmpty(response.Token.Token) )
                {
                    var test = HttpContext.User;
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }
}
