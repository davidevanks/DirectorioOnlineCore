using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AppDirectorioWeb.Models.Identity.AccountViewModels;
using AppDirectorioWeb.RequestProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
              
                //only for dev enviroment
               
                var response = await _backendHelper.PostAsync<ResponseViewModel>("/api/Account/api/Login", model);
                
                if (response.MessageResponseCode == ResponseViewModel.MessageCode.Success && !String.IsNullOrEmpty(response.Token.Token) )
                {
                   
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
