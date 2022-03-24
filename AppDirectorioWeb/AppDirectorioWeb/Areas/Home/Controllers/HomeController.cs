

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
           
            _backendApiUrlNegocio = configuration["BackendApiUrlNegocio"];
            _backendApiUrlSeguridad = configuration["BackendApiUrlSeguridad"];
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Error()
        {
          
            //e.MessageError = "no tienes acceso a esta página";
            return View();
        }

        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            
            return View();
           
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