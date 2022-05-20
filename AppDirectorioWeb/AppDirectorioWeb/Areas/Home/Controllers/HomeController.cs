using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace AppDirectorioWeb.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
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
        

        public IActionResult ContactUs()
        {
           
            return View();
        }

        #endregion Public Methods

#region API_CALLS
        [HttpPost]
        public IActionResult SendMessageContactUs([FromBody] ContactUsViewModel model)
        {
            try
            {
                string MailText;
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateContactUsEmail.html";
                StreamReader str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("%asunto%", model.Subject).Replace("%NombreCompania%",model.CompanyName).Replace("%NombreCompleto%", model.PersonName).Replace("%Email%", model.Email).Replace("%NumeroTelefono%", model.Phone).Replace("%Mensaje%", model.Message);
              
                //cambiar correo cuando tengamos los reales
               var t= _emailSender.SendEmailAsync("davidevanks@mailinator.com", "Mensaje Desde Formulario Contactos Brujula Pyme", MailText);

                return Json(new { success = true, message = "Mensaje Enviado!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error Al Enviar Mensaje!" });
            }
        }
#endregion
    }
}