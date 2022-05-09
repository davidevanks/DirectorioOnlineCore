using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Controllers
{
    [Area("Negocios")]
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/

        #region Private Fields

        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DirectorioOnlineCoreContext context;
        private readonly IWebHostEnvironment hostingEnvironment;

        #endregion Private Fields

        #region Public Constructors

        public NegociosController(DirectorioOnlineCoreContext context,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AgregarNegocio()
        {
            AddUpdBusinessViewModel model = new AddUpdBusinessViewModel();
            //Se agrega cataegorias y departamentos
            model.Business = new BussinesViewModel()
            {
                Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 1 && x.Activo==true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }),

                Departamentos = _unitOfWork.Departament.GetAll().Where(x => x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                })
            };

            //Se agrega data de días
            var Days = _unitOfWork.Category.GetAll(x => x.IdPadre == 25 && x.Activo == true).ToList();

            List<HorarioNegocioViewModel> ScheduleDayList = new List<HorarioNegocioViewModel>();
            foreach (var d in Days)
            {
                HorarioNegocioViewModel ScheduleDay = new HorarioNegocioViewModel();
                ScheduleDay.Day = d.Nombre;
                ScheduleDay.IdDia = d.Id;
                ScheduleDay.CreateDate = DateTime.Now;
                ScheduleDay.IdUserCreate = "0";
                ScheduleDayList.Add(ScheduleDay);
            }

            model.HorarioNegocios = ScheduleDayList;

            //Se agrega data para mostrar features
            List<FeatureNegocioViewModel> FeatureNegocios = new List<FeatureNegocioViewModel>();
            var Features = _unitOfWork.Category.GetAll(x => x.IdPadre == 20 && x.Activo == true).ToList();
            foreach (var fn in Features)
            {
                FeatureNegocioViewModel feature = new FeatureNegocioViewModel();
                feature.IdFeature = fn.Id;
                feature.Feature = fn.Nombre;
                feature.CreateDate = DateTime.Now;
                feature.IdUserCreate = "0";
                FeatureNegocios.Add(feature);
            }

            model.FeatureNegocios = FeatureNegocios;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarNegocio(AddUpdBusinessViewModel model)
        {
        
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.User.Email, Email = model.User.Email, PhoneNumber = model.User.Telefono };

                var result = await _userManager.CreateAsync(user, model.User.Password);

                string idUserCreate = "";
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    idUserCreate = user.Id;
                }
                else
                {
                    idUserCreate = HttpContext.Session.GetString("UserId");
                }
                //asignar rol
                await _userManager.AddToRoleAsync(user, SP.Role_BusinesAdmin);

                //guardamos el detalle del usuario.

                var userDetail = new UserDetail { UserId = user.Id, FullName = model.User.FullName, NotificationsPromo = true, RegistrationDate = DateTime.Now, IdUserCreate = idUserCreate };
                _unitOfWork.UserDetail.Add(userDetail);

                //asignamos el id del usuario a su negocio(idUserCreate)
                model.Business.IdUserCreate = idUserCreate;
                model.Business.CreateDate = DateTime.Now;
                model.Business.IdUserOwner = user.Id;
                model.Business.Status = 19;

                //logica para logo
                string uniqueFileName = SaveLogoPicture(model);
                model.Business.LogoNegocio = uniqueFileName;

                var negocio = _mapper.Map<Negocio>(model.Business);
                _unitOfWork.Business.Add(negocio);
                _unitOfWork.Save();

                SaveFeaturesBusiness(model, negocio, idUserCreate);

                SaveSchedulesBusiness(model, negocio, idUserCreate);

                //logica para galerias de imagenes
                SavePicturesBusiness(model, negocio, idUserCreate);

                _unitOfWork.Save();

                //codigo para envio de correo de verificación de cuenta
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string MailText = GetEmailActivationUserAdminBusiness(user, code);
                await _emailSender.SendEmailAsync(user.Email, "Verificación de Cuenta", MailText);

                //

                return RedirectToAction(nameof(ConfirmationBusinessRegistration));
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ConfirmationBusinessRegistration()
        {
            return View();
        }

    

        [Authorize(Roles = SP.Role_BusinesAdmin+","+SP.Role_Admin)]
        public IActionResult AdminBusiness()
        {
            string idOwner = "-1";
            if (User.IsInRole(SP.Role_BusinesAdmin))
            {
                idOwner = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            }

            ViewBag.idOwner = idOwner;
            return View();
        }

        public IActionResult GetDetailByBussinesId(int id)
        {

            return View();
        }

        #endregion Public Methods

        #region API_CALLS
        [HttpGet]
        public IActionResult GetBusinessByOwners(string idOwner)
        {
            var parentsObj = _unitOfWork.Business.GetBusinessByOwners(idOwner);
            return Json(new { data = parentsObj });
        }

        [HttpPost]
        public IActionResult ManageBusinesActivation([FromBody] string id)
        {
            var business = _unitOfWork.Business.Get(Convert.ToInt32(id));
            var emailUser = _userManager.FindByIdAsync(business.IdUserOwner).Result.Email;
           

            string message = "";
            if (business == null)
            {
                return Json(new { success = false, message = "Negocio no existe!" });
            }

            if (business.Status==19)
            {
                //user is currently locked, we will unlock
                business.Status = 17;
                message = " Aprobado!";
            }
            else if (business.Status == 17)
            {
                business.Status = 18;
                message = " Desactivado!";
            }
            else if (business.Status == 18)
            {
                business.Status = 17;
                message = " Activado!";
            }



            _unitOfWork.Business.Update(business);
            _unitOfWork.Save();


            //ENVIO DE MENSAJE DE CAMBIO DE STATUS
            string MailText;
            var callbackUrl = Url.Action(
              "AdminBusiness",
              "Negocios",
              values: new {  idOwner = business.IdUserOwner },
              protocol: Request.Scheme);

            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateBusinessStatusNotification.html";
            StreamReader str = new StreamReader(FilePath);
            MailText = str.ReadToEnd();
            str.Close();

             MailText  = MailText.Replace("[username]", emailUser).Replace("[linkRef]", HtmlEncoder.Default.Encode(callbackUrl));
            MailText = MailText.Replace("[negocioName]", business.NombreNegocio);

            _emailSender.SendEmailAsync(emailUser, "Notificación cambio de status negocio", MailText);
            //-----------------------------------


            return Json(new { success = true, message = "Negocio" + message });
        }

        #endregion

        #region MetodosAuxiliares

        public string GetEmailActivationUserAdminBusiness(IdentityUser user, string codeG)
        {
            string MailText = "";
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");
            var code = codeG;
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateConfirmEmail.html";
            StreamReader str = new StreamReader(FilePath);
            MailText = str.ReadToEnd();
            str.Close();

            return MailText = MailText.Replace("[username]", user.Email).Replace("[linkRef]", HtmlEncoder.Default.Encode(callbackUrl));
        }

        public void SaveFeaturesBusiness(AddUpdBusinessViewModel model, Negocio negocio, string idUserCreate)
        {
            foreach (var feature in model.FeatureNegocios)
            {
                feature.IdNegocio = negocio.Id;
                feature.IdUserCreate = idUserCreate;
                feature.CreateDate = DateTime.Now;
            }

            var features = _mapper.Map<List<FeatureNegocio>>(model.FeatureNegocios);
            _unitOfWork.Feature.InsertList(features);
        }

        public string SaveLogoPicture(AddUpdBusinessViewModel model)
        {
            string uniqueFileName = "";
            if (model.Logo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImagesBusiness");
                uniqueFileName = Guid.NewGuid().ToString() + "_logoBusiness_" + model.Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }

        public void SavePicturesBusiness(AddUpdBusinessViewModel model, Negocio negocio, string idUserCreate)
        {
            string uniqueFileNames = null;
            List<ImagenesNegocioViewModel> lstPictures = new List<ImagenesNegocioViewModel>();
            if (model.PicturesBusiness != null && model.PicturesBusiness.Count > 0)
            {
                foreach (IFormFile picture in model.PicturesBusiness)
                {
                    ImagenesNegocioViewModel pic = new ImagenesNegocioViewModel();
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImagesBusiness");
                    uniqueFileNames = Guid.NewGuid().ToString() + "_picturesBusiness_" + picture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileNames);
                    picture.CopyTo(new FileStream(filePath, FileMode.Create));

                    pic.IdNegocio = negocio.Id;
                    pic.IdUserCreate = idUserCreate;
                    pic.CreateDate = DateTime.Now;
                    pic.Image = uniqueFileNames;
                    lstPictures.Add(pic);
                }

                var picturesBusiness = _mapper.Map<List<ImagenesNegocio>>(lstPictures);
                _unitOfWork.ImageBusiness.InsertList(picturesBusiness);
            }
        }

        public void SaveSchedulesBusiness(AddUpdBusinessViewModel model, Negocio negocio, string idUserCreate)
        {
            foreach (var sche in model.HorarioNegocios)
            {
                sche.IdNegocio = negocio.Id;
                sche.IdUserCreate = idUserCreate;
                sche.CreateDate = DateTime.Now;
            }

            var schedules = _mapper.Map<List<HorarioNegocio>>(model.HorarioNegocios);
            _unitOfWork.ScheduleBusiness.InsertList(schedules);
        }

        #endregion MetodosAuxiliares
    }
}