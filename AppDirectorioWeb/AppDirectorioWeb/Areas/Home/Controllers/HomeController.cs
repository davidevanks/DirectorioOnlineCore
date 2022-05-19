

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AppDirectorioWeb.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly string _backendApiUrlNegocio;
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        private readonly string _backendApiUrlSeguridad;
        private readonly IUnitOfWork _unitOfWork;
        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
           
            _backendApiUrlNegocio = configuration["BackendApiUrlNegocio"];
            _backendApiUrlSeguridad = configuration["BackendApiUrlSeguridad"];
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //e.MessageError = "no tienes acceso a esta página";
            return View();
        }

        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            LookForBusinessViewModel model = new LookForBusinessViewModel();
            model.IdDepartamento = "10";

            model.Departamentos = _unitOfWork.Departament.GetAll().Where(x => x.IdPais == 1 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });

            model.Categories = _unitOfWork.Category.GetCatUsedByBusiness().Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });

            return View(model);
           
        }

        //[Authorize(Roles.Admin)]
        public async Task<IActionResult> Privacy()
        {
            string url = _backendApiUrlNegocio + "/api/CatCatalogo/api/GetAllCatalogo";

         
            var JWToken = HttpContext.Session.GetString("Token");
          
            return View();
        }

        #endregion Public Methods

        #region Private Methods

        public async Task<IActionResult> SeachBussines()
        {
         
          
      
            return View();

        }

        #endregion Private Methods
    }
}