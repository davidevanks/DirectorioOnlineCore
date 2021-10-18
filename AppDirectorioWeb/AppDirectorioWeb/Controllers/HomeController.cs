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

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IDecode decode, IBackendHelper backendHelper, IConfiguration configuration)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _decode = decode;
            _backendHelper = backendHelper;
            _backendApiUrlNegocio = configuration["BackendApiUrlNegocio"];
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Error()
        {
            ErrorViewModel e = new ErrorViewModel();
            e.MessageError = "no tienes acceso a esta página";
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles.Admin)]
        public async Task<IActionResult> Privacy()
        {
            string url = _backendApiUrlNegocio + "/api/CatCatalogo/api/GetAllCatalogo";

            string Role = GetRole();
            var JWToken = HttpContext.Session.GetString("Token");
            var test = await _backendHelper.GetAsync<IEnumerable<CatCatalogosViewModel>>(url, JWToken);
            return View();
        }

        #endregion Public Methods

        #region Private Methods

        private string GetRole()
        {
            if (this.HavePermission(Roles.Admin))
                return Roles.Admin;
            if (this.HavePermission(Roles.PlanMiPyme))
                return Roles.PlanMiPyme;
            if (this.HavePermission(Roles.PlanEmpresarial))
                return Roles.PlanEmpresarial;
            if (this.HavePermission(Roles.PlanTrabajadorAutonomo))
                return Roles.PlanTrabajadorAutonomo;
            return null;
        }

        #endregion Private Methods
    }
}