using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Controllers
{
    [Area("Home")]
   
    public class HomeController : Controller
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IMailJetSender _mailJetSender;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, 
            IUnitOfWork unitOfWork, 
            IEmailSender emailSender, 
            IMailJetSender mailJetSender,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mailJetSender = mailJetSender;
            _userManager = userManager;
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

            //if (exceptionHandlerPathFeature!=null && exceptionHandlerPathFeature.Error!=null)
            //{
            //    LogError log = new LogError();
            //    log.Date = DateTime.Now;
            //    log.MessageError = exceptionHandlerPathFeature.Error.Message;
            //    log.Status = true;
            //    log.Observation = "";

            //    _unitOfWork.Log.Add(log);
            //    _unitOfWork.Save();

            //}

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
        public async Task<IActionResult> SendMessageContactUs([FromBody] ContactUsViewModel model)
        {
            try
            {
                string MailText;
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateContactUsEmail.html";
                StreamReader str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                str.Close();
              
                MailText = MailText.Replace("%asunto%", model.Subject).Replace("%NombreCompania%",model.CompanyName).Replace("%NombreCompleto%", model.PersonName).Replace("%Email%", model.Email).Replace("%NumeroTelefono%", model.Phone).Replace("%Mensaje%", model.Message);


            var response=    await _mailJetSender.SendEmailAsync("brujulapymenic@gmail.com", "Mensaje Formulario Contactos Brujula Pyme", MailText);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Mensaje Enviado!" });
                }
                else
                {
                    return Json(new { success = false, message = "Mensaje No Enviado!" });
                }

                
            }
            catch (Exception ex)
            {
                return Json(new {error=ex.Message+"-"+ex.StackTrace, success = false, message = "Error Al Enviar Mensaje!" });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessageToOwner([FromBody] ContactOwnerViewModel model)
        {
            try
            {
               var userId= _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

                var userDetail = _unitOfWork.UserDetail.GetAUsersDetails(userId).FirstOrDefault() ;

                string MailText;
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateEmailToOwner.html";
                StreamReader str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                str.Close();

                var nameBusiness = _unitOfWork.Business.GetBusinessById(model.BusinessId);

                MailText = MailText.Replace("[NameOwner]", nameBusiness.NombreNegocio).Replace("[NamePerson]", userDetail.FullName).Replace("[EmailCustomer]", userDetail.Email).Replace("[PhoneCustomer]", userDetail.PhoneNumber).Replace("[MessageCustomer]", model.Message);


                var response = await _mailJetSender.SendEmailAsync(nameBusiness.EmailNegocio, "Mensaje Formulario Contactos Brújula Pyme", MailText);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Mensaje Enviado!" });
                }
                else
                {
                    return Json(new { success = false, message = "Mensaje No Enviado!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, success = false, message = "Error Al Enviar Mensaje!" });
            }
        }
        #endregion
    }
}