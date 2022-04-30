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

namespace AppDirectorioWeb.Controllers
{
    [Area("Negocios")]
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        private readonly DirectorioOnlineCoreContext context;

        public NegociosController(DirectorioOnlineCoreContext context,/*ILogger<NegociosController> logger,*/ IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
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

                FeatureNegocios.Add(feature);
            }

            model.FeatureNegocios = FeatureNegocios;
            return View(model);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<JsonResult> NegociosNuevo()
        {
         
            return Json("");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetDetailByBussinesId(int id)
        {
        
            return View();
        }
    }
}
