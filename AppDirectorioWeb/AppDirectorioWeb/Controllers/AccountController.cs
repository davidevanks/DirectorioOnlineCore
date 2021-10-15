﻿using AppDirectorioWeb.RequestProvider.Interfaces;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Identity.AccountViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Controllers
{
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly IBackendHelper _backendHelper;
        private readonly IDecode _decode;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(IBackendHelper backendHelper, IDecode decode)
        {
            _backendHelper = backendHelper;
            _decode = decode;
        }

        #endregion Public Constructors

        #region Public Methods

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

                if (response.MessageResponseCode == ResponseViewModel.MessageCode.Success && !String.IsNullOrEmpty(response.Token.Token))
                {
                    var token = _decode.DecodeToken(response.Token.Token);
                    int expiration = Convert.ToInt32(token.Claims.First(c => c.Type == "DurationToken").Value);
                    HttpContext.Session.SetString("Token", response.Token.Token);
                    return LocalRedirect(returnUrl);
                }
            }

            return View(model);
        }

        #endregion Public Methods
    }
}