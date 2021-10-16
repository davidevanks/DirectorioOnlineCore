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
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            //returnUrl = context.Result;
            ReturnUrl ??= Url.Content("~/");
            ViewData["MessageErrorLogin"] = "";
            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                var response = await _backendHelper.PostAsync<ResponseViewModel>("/api/Account/api/Login", model);

                if (response.MessageResponseCode == ResponseViewModel.MessageCode.Success && !String.IsNullOrEmpty(response.Token.Token))
                {
                    var token = _decode.DecodeToken(response.Token.Token);
                    int expiration = Convert.ToInt32(token.Claims.First(c => c.Type == "DurationToken").Value);
                    HttpContext.Session.SetString("Token", response.Token.Token);

                    if (String.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return LocalRedirect(ReturnUrl);
                }

                if (response.MessageResponseCode == ResponseViewModel.MessageCode.IncorrectPassword || response.MessageResponseCode == ResponseViewModel.MessageCode.UserNotExist || response.MessageResponseCode == ResponseViewModel.MessageCode.InvalidInformation)
                {
                    ViewData["MessageErrorLogin"] = "Email o password inválidos";
                }

                if (response.MessageResponseCode == ResponseViewModel.MessageCode.EmailNotConfirmed)
                {
                    ViewData["MessageErrorLogin"] = "Por favor revise su correo y confirme su cuenta para poder ingresar";
                }
                if (response.MessageResponseCode == ResponseViewModel.MessageCode.Failed)
                {
                    ViewData["MessageErrorLogin"] = "Ha ocurrido un error.";
                }
            }

            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
           
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }

        #endregion Public Methods
    }
}