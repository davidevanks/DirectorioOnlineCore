using AppDirectorioWeb.Models;
using AppDirectorioWeb.RequestProvider.Interfaces;
using AppDirectorioWeb.Utiles.CustomAttributes;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Models;
using Models.Models.Identity.AccountViewModels;

namespace AppDirectorioWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly string _backendApiUrlNegocio;
        private readonly IBackendHelper _backendHelper;
        private readonly IDecode _decode;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        private readonly string _backendApiUrlSeguridad;
        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IDecode decode, IBackendHelper backendHelper, IConfiguration configuration)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _decode = decode;
            _backendHelper = backendHelper;
            _backendApiUrlNegocio = configuration["BackendApiUrlNegocio"];
            _backendApiUrlSeguridad = configuration["BackendApiUrlSeguridad"];
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Error()
        {
            ErrorViewModel e = new ErrorViewModel();
            e.MessageError = "no tienes acceso a esta página";
            return View();
        }

        public async Task<IActionResult> Index()
        {
            //remover en produccion
            var JWToken = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(JWToken))
            {
                LoginViewModel model = new LoginViewModel();
                model.Email = "davidevanks@gmail.com";
                model.Password = "12345678";
                var response = await _backendHelper.PostAsync<ResponseViewModel>(_backendApiUrlSeguridad + "/api/Account/api/Login", model);
                HttpContext.Session.SetString("Token", response.Token.Token);
                return RedirectToAction("Index", "Home");
            }
           
            
            //-----------remover en prod
            return View();
           
        }

        //[Authorize(Roles.Admin)]
        public async Task<IActionResult> Privacy()
        {
            string url = _backendApiUrlNegocio + "/api/CatCatalogo/api/GetAllCatalogo";

         
            var JWToken = HttpContext.Session.GetString("Token");
            var test = await _backendHelper.GetAsync<IEnumerable<CatCatalogosViewModel>>(url, JWToken);
            return View();
        }

        #endregion Public Methods

        #region Private Methods

       

        #endregion Private Methods
    }
}