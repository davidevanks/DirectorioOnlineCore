﻿using AutoMapper;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        #endregion Private Fields

        #region Public Constructors

        public NegociosController(DirectorioOnlineCoreContext context,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostingEnvironment,
            SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            this.hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AgregarNegocio(int? Id)
        {
            AddUpdBusinessViewModel model = new AddUpdBusinessViewModel();

            if (Id != null)
            {
                model.Business = _unitOfWork.Business.GetBusinessToEditById((int)Id);

                if (model.Business == null)
                {
                    return NotFound();
                }

                model.Business.Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 1 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });

                model.Business.Departamentos = _unitOfWork.Departament.GetAll().Where(x => x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });

                model.ImagenesNegocios = _unitOfWork.ImageBusiness.GetImagesByBusinessId((int)Id);
            }
            else
            {
                //Se agrega cataegorias y departamentos
                model.Business = new BussinesViewModel()
                {
                    Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 1 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
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
            }

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

            model.HorarioNegocios = (Id == null) ? ScheduleDayList : _unitOfWork.ScheduleBusiness.GetScheduleListToEditByBusinessId((int)Id);

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

            model.FeatureNegocios = (Id == null) ? FeatureNegocios : _unitOfWork.Feature.GetListFeaturesToEditByBusinessId((int)Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarNegocio(AddUpdBusinessViewModel model)
        {
            if (ModelState.IsValid)
            {
                if ((model.Business.Id==0 || model.Business.Id == null) && !_signInManager.IsSignedIn(User))
                {
                    //registro negocio nuevo y usuario nuevo
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
                else if((model.Business.Id != 0 && model.Business.Id != null) && _signInManager.IsSignedIn(User))
                {
                    //actualización negocio
                    //asignamos el id del usuario a su negocio(idUserCreate)
                  
                   
                    model.Business.IdUserUpdate = HttpContext.Session.GetString("UserId");
                    model.Business.UpdateDate = DateTime.Now;
                    model.Business.Status = 19;//vuelve al estado en aprobación ya que se debe verificar datos actualizados

                    //logica para logo
                    //volvemos a consultar negocio para reeactualizar estado de logo actual
                    var logB = _unitOfWork.Business.GetBusinessToEditById((int)model.Business.Id);
                    model.Business.LogoNegocio = logB.LogoNegocio;
                    if (model.Logo != null)
                    {
                        string uniqueFileName = SaveLogoPicture(model);
                        model.Business.LogoNegocio = uniqueFileName;
                    }
                   

                    var negocio = _mapper.Map<Negocio>(model.Business);
                    _unitOfWork.Business.Update(negocio);

                    //update features
                    UpdateFeaturesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    UpdateSchedulesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    //logica para galerias de imagenes
                    SavePicturesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    _unitOfWork.Save();
                    return RedirectToAction(nameof(UpdateSaveBusinessRegistration));
                    
                }
                else if ((model.Business.Id == 0 || model.Business.Id == null) && _signInManager.IsSignedIn(User))
                {
                    //negocio nuevo pero con usuario ya registrado
                    //asignamos el id del usuario a su negocio(idUserCreate)
                    model.Business.IdUserUpdate = HttpContext.Session.GetString("UserId");
                    model.Business.UpdateDate = DateTime.Now;
                    model.Business.Status = 19;//vuelve al estado en aprobación ya que se debe verificar datos actualizados

                    //logica para logo
                    string uniqueFileName = SaveLogoPicture(model);
                    model.Business.LogoNegocio = uniqueFileName;

                    var negocio = _mapper.Map<Negocio>(model.Business);
                    _unitOfWork.Business.Add(negocio);
                    _unitOfWork.Save();

                    SaveFeaturesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    SaveSchedulesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    //logica para galerias de imagenes
                    SavePicturesBusiness(model, negocio, HttpContext.Session.GetString("UserId"));

                    _unitOfWork.Save();
                    return RedirectToAction(nameof(UpdateSaveBusinessRegistration));

                }

            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ConfirmationBusinessRegistration()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult UpdateSaveBusinessRegistration()
        {
            return View();
        }

        public IActionResult GetDetailByBussinesId(int id)
        {
            DetailsBusinessViewModel BusinessDetails = new DetailsBusinessViewModel();
            BusinessDetails.Business = _unitOfWork.Business.GetBusinessById(id);
            BusinessDetails.FeatureNegocios = _unitOfWork.Feature.GetListFeaturesByBusinessId(id);
            BusinessDetails.HorarioNegocios = _unitOfWork.ScheduleBusiness.GetScheduleListByBusinessId(id);
            BusinessDetails.ImagenesNegocios = _unitOfWork.ImageBusiness.GetImagesByBusinessId(id);

            foreach (var item in BusinessDetails.ImagenesNegocios)
            {
                string uploadsFolder = "/ImagesBusiness/";
                string uniqueFileNames = item.Image;
                string filePath = Path.Combine(uploadsFolder, uniqueFileNames);
                item.Image = filePath;
            }

            return View(BusinessDetails);
        }

        #endregion Public Methods

        #region API_CALLS

        [HttpGet]
        public IActionResult DeleteLogo(int id)
        {
            var Business = _unitOfWork.Business.Get(id);
            if (Business == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImagesBusiness");
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), uploadsFolder, Business.LogoNegocio);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else
            {
                Business.LogoNegocio = "";
                _unitOfWork.Business.Update(Business);
                _unitOfWork.Save();
                return Json(new { success = false, message = "Error al borrar, directorio no existe" });
            }

            Business.LogoNegocio = "";
            _unitOfWork.Business.Update(Business);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Se borro logo exitosamente" });
        }

        [HttpGet]
        public IActionResult DeletePictures(int id)
        {
            var pictures = _unitOfWork.ImageBusiness.GetRangeImagesToDeleteByBusinessId(id);
            if (pictures == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            foreach (var item in pictures)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImagesBusiness");
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), uploadsFolder, item.Image);

                if (System.IO.File.Exists(path))
                    {
                  
                    System.IO.File.Delete(path);

                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al borrar, directorio no existe" });
                    }

                    
             
             
            }

            _unitOfWork.ImageBusiness.RemoveRange(pictures);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Se borraron Imagen(es) exitosamente!" });
        }

        [HttpGet]
        public IActionResult GetBusinessByOwners(string idOwner)
        {
            var parentsObj = _unitOfWork.Business.GetListBusinessByOwners(idOwner);
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

            if (business.Status == 19)
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
              values: new { idOwner = business.IdUserOwner },
              protocol: Request.Scheme);

            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateBusinessStatusNotification.html";
            StreamReader str = new StreamReader(FilePath);
            MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[username]", emailUser).Replace("[linkRef]", HtmlEncoder.Default.Encode(callbackUrl));
            MailText = MailText.Replace("[negocioName]", business.NombreNegocio);

            _emailSender.SendEmailAsync(emailUser, "Notificación cambio de status negocio", MailText);
            //-----------------------------------

            return Json(new { success = true, message = "Negocio" + message });
        }

        #endregion API_CALLS

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

        public void UpdateFeaturesBusiness(AddUpdBusinessViewModel model, Negocio negocio, string idUserUpdate)
        {
            foreach (var feature in model.FeatureNegocios)
            {
               
                feature.IdUserUpdate = idUserUpdate;
                feature.UpdateDate = DateTime.Now;
            }

            var features = _mapper.Map<List<FeatureNegocio>>(model.FeatureNegocios);
            foreach (var item in features)
            {
                _unitOfWork.Feature.Update(item);
            }
          
        }

        public string SaveLogoPicture(AddUpdBusinessViewModel model)
        {
            string uniqueFileName = "";
            if (model.Logo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImagesBusiness");
                uniqueFileName = Guid.NewGuid().ToString() + "_logoBusiness_" + model.Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                FileStream s = new FileStream(filePath, FileMode.Create);
                model.Logo.CopyTo(s);

                s.Close();
                s.Dispose();
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
                    FileStream s = new FileStream(filePath, FileMode.Create);
                    picture.CopyTo(s);

                    pic.IdNegocio = negocio.Id;
                    pic.IdUserCreate = idUserCreate;
                    pic.CreateDate = DateTime.Now;
                    pic.Image = uniqueFileNames;
                    lstPictures.Add(pic);

                    s.Close();
                    s.Dispose();
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
        public void UpdateSchedulesBusiness(AddUpdBusinessViewModel model, Negocio negocio, string idUserUpdate)
        {
            foreach (var sche in model.HorarioNegocios)
            {
                
                sche.IdUserUpdate = idUserUpdate;
                sche.UpdateDate = DateTime.Now;
            }

            var schedules = _mapper.Map<List<HorarioNegocio>>(model.HorarioNegocios);

            foreach (var item in schedules)
            {
                _unitOfWork.ScheduleBusiness.Update(item);
            }
            
           
        }

        #endregion MetodosAuxiliares
    }
}