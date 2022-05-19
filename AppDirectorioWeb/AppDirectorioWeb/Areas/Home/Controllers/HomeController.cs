using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using System.Linq;

namespace AppDirectorioWeb.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            return View();
        }

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

        #endregion Public Methods
    }
}