using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AppDirectorioWeb.RequestProvider.Interfaces;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Models.Models.Identity.AccountViewModels;

namespace AppDirectorioWeb.Controllers
{
    
   
    public class AccountController : Controller
    {

        private readonly IBackendHelper _backendHelper;
        private readonly IDecode _decode;
        public AccountController(IBackendHelper backendHelper, IDecode decode)
        {
            _backendHelper = backendHelper;
            _decode = decode;

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
                    var token = _decode.DecodeToken(response.Token.Token);
                    int expiration =Convert.ToInt32(token.Claims.First(c => c.Type == "DurationToken").Value);
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(expiration);
                    option.HttpOnly = true;
                    Response.Cookies.Append("Token", response.Token.Token, option);
                    return LocalRedirect(returnUrl);
                }
           


            }


            return View(model);
            // If we got this far, something failed, redisplay form

        }


    }
}
