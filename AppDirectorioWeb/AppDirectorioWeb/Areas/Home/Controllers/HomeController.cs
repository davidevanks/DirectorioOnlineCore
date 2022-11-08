using DataAccess.Models;
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
        private readonly ILogger<HomeController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods
       
        public IActionResult Error(int? statusCode = null)
        {
            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        
            switch (statusCode)
            {
                case 404:
                    _logger.LogError("Esta página no existe!");
                    break;
            

            }

            if (exceptionHandlerPathFeature!=null && exceptionHandlerPathFeature.Error!=null)
            {
                LogError log = new LogError();
                log.Date = DateTime.Now;
                log.MessageError = exceptionHandlerPathFeature.Error.Message;
                log.Status = true;
                log.Observation = "";

                _unitOfWork.Log.Add(log);
                _unitOfWork.Save();

            }

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
        public IActionResult GetHowItWorks()
        {

            return View();
        }

        public IActionResult GetCourses()
        {

            return View();
        }

        public IActionResult GetWhoWeAre()
        {

            return View();
        }

        public IActionResult GetTermsAndConditions()
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
              
               
               var t= _emailSender.SendEmailAsync("info@brujulapyme.com", "Mensaje Formulario Contactos Brujula Pyme", MailText);

                return Json(new { success = true, message = "Mensaje Enviado!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error Al Enviar Mensaje!" });
            }
        }

        [HttpPost]
        public IActionResult SendMessageToOwner([FromBody] ContactOwnerViewModel model)
        {
            try
            {
                string MailText;
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateEmailToOwner.html";
                StreamReader str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                str.Close();

                var nameBusiness = _unitOfWork.Business.GetBusinessById(model.BusinessId);

                MailText = MailText.Replace("[NameOwner]", nameBusiness.NombreNegocio).Replace("[NamePerson]", model.PersonName).Replace("[EmailCustomer]", model.Email).Replace("[PhoneCustomer]", model.Phone).Replace("[MessageCustomer]", model.Message);


                var t = _emailSender.SendEmailAsync(nameBusiness.EmailNegocio, "Mensaje Formulario Contactos Brujula Pyme", MailText);

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