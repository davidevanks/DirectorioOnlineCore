using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Authorization;
using System.IO;
using DataAccess.Models;
using Models.ViewModels;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppDirectorioWeb.Controllers
{
    [Area("Negocios")]
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly DirectorioOnlineCoreContext context;

        public NegociosController(DirectorioOnlineCoreContext context,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public  IActionResult AgregarNegocio()
        {
            AddUpdBusinessViewModel model = new AddUpdBusinessViewModel();
            //Se agrega cataegorias y departamentos
            model.Business = new BussinesViewModel()
            {
                Categories=_unitOfWork.Category.GetAll().Where(x=>x.IdPadre==1).Select(x=>new { x.Id,x.Nombre}).Select(i=>new SelectListItem 
                { 
                Text=i.Nombre,
                Value=i.Id.ToString()
                }),

                Departamentos = _unitOfWork.Departament.GetAll().Where(x => x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                })
            };

            //Se agrega data de días
            var Days = _unitOfWork.Category.GetAll(x => x.IdPadre == 25).ToList();
           
            
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
            var Features = _unitOfWork.Category.GetAll(x => x.IdPadre == 20).ToList();
            foreach (var fn in Features)
            {
                FeatureNegocioViewModel feature = new FeatureNegocioViewModel();
                feature.IdFeature = fn.Id;
                feature.Feature = fn.Nombre;
                feature.CreateDate = DateTime.Now;
                feature.IdUserCreate   = "0";
                FeatureNegocios.Add(feature);
            }

            model.FeatureNegocios = FeatureNegocios;
            return View(model);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult AgregarNegocio(AddUpdBusinessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.User.Email, Email = model.User.Email, PhoneNumber = model.User.Telefono };

                var result =  _userManager.CreateAsync(user, model.User.Password);


                //asignamos el id del usuario a su negocio(idUserCreate)
                model.Business.IdUserCreate = user.Id;
                model.Business.CreateDate = DateTime.Now;

                //logica para logo

                var negocio = _mapper.Map<Negocio>(model.Business);
           

                _unitOfWork.Business.Add(negocio);
                _unitOfWork.Save();


                foreach (var feature in model.FeatureNegocios)
                {
                    feature.IdNegocio = negocio.Id;
                    feature.IdUserCreate = user.Id;

                }


                var features = _mapper.Map<List<FeatureNegocio>>(model.FeatureNegocios);
                _unitOfWork.Feature.InsertList(features);

                foreach (var sche in model.HorarioNegocios)
                {
                    sche.IdNegocio = negocio.Id;
                    sche.IdUserCreate = user.Id;
                }

                var schedules = _mapper.Map<List<HorarioNegocio>>(model.HorarioNegocios);
                _unitOfWork.ScheduleBusiness.InsertList(schedules);


                //logica para galerias de imagenes
            }

            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetDetailByBussinesId(int id)
        {
        
            return View();
        }
    }
}
