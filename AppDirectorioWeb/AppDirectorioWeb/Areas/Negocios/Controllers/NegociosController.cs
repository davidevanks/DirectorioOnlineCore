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
