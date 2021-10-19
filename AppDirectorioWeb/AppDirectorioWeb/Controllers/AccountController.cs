using AppDirectorioWeb.RequestProvider.Interfaces;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Identity.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelApp.Models;

namespace AppDirectorioWeb.Controllers
{
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly IBackendHelper _backendHelper;
        private readonly IDecode _decode;
        private readonly string _backendApiUrlSeguridad;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(IBackendHelper backendHelper, IDecode decode, IConfiguration configuration)
        {
            _backendHelper = backendHelper;
            _decode = decode;
            _backendApiUrlSeguridad= configuration["BackendApiUrlSeguridad"];
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
                var response = await _backendHelper.PostAsync<ResponseViewModel>(_backendApiUrlSeguridad+"/api/Account/api/Login", model);

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
        public async Task<IActionResult> Register(string IsBussines)
        {
            List<PlanViewModel> lstPlanes = new List<PlanViewModel>();
            ViewData["IsBussines"] = IsBussines;
            if (IsBussines=="1")
            {
                lstPlanes= await _backendHelper.GetAsync<List<PlanViewModel>>("/api/Account/api/GetPlans");
            }
      
            //hacemos request para mandar a traer los planes disponibles.
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