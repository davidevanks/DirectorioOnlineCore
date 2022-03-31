using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;

namespace AppDirectorioWeb.Areas.Catalogos.Controllers
{
    [Area("Catalogos")]
    public class CatCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CatCategoryViewModel category = new CatCategoryViewModel();
            if (id==null)
            {
                return View(category);
            }

            var cat = _unitOfWork.Category.Get(id.GetValueOrDefault());

          
           

            if (cat == null)
            {
                return NotFound();
            }
           
                
            category.Id = cat.Id;
            category.IdPadre = cat.IdPadre;
            category.Nombre = cat.Nombre;
            category.Activo = (cat.Activo != null && cat.Activo != false);
                
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CatCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id==0)
                {
                    _unitOfWork.Category.Add(new CatCategorium()
                    {
                        Id = model.Id,
                        IdPadre = 0,
                        Nombre = model.Nombre,
                        Activo = model.Activo
                    });

                 
                }
                else
                {
                    _unitOfWork.Category.Update(new CatCategorium()
                        {
                            Id = model.Id,
                            IdPadre = 0,
                            Nombre = model.Nombre,
                            Activo = model.Activo
                        });
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpDelete]
        public IActionResult DeleteParentCat(int id)
        {
            var cat = _unitOfWork.Category.Get(id);
            if (cat==null)
            {
                return Json(new{success=false,message="Error al borrar"});
            }

            _unitOfWork.Category.Remove(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Se borro categoría padre exitosamente" });
        }

        #region API_CALLS

        [HttpGet]
         public IActionResult GetAllParents()
         {
             var parentsObj = _unitOfWork.Category.GetAll(x => x.IdPadre == 0);
             return Json(new{data= parentsObj });
         }
         #endregion
    }
}
