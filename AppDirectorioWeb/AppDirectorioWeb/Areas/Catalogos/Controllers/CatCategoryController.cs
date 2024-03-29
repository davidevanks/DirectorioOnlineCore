﻿using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using Utiles;

namespace AppDirectorioWeb.Areas.Catalogos.Controllers
{
    [Area("Catalogos")]
    [Authorize(Roles = SP.Role_Admin)]
    public class CatCategoryController : Controller
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public CatCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult AddCatChild(int id)
        {
            return View(id);
        }

        [HttpDelete]
        public IActionResult DeleteParentCat(int id)
        {
            var cat = _unitOfWork.Category.Get(id);
            if (cat == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _unitOfWork.Category.Remove(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Se borro categoría padre exitosamente" });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CatCategoryViewModel category = new CatCategoryViewModel();
            if (id == null)
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
                if (model.Id == 0)
                {
                    _unitOfWork.Category.Add(new CatCategorium()
                    {
                        Id = model.Id,
                        IdPadre = 0,
                        Nombre = model.Nombre,
                        Activo = model.Activo,
                        IdUserCreate = HttpContext.Session.GetString("UserId"),
                        CreateDate = DateTime.Now
                    });
                }
                else
                {
                    _unitOfWork.Category.Update(new CatCategorium()
                    {
                        Id = model.Id,
                        IdPadre = 0,
                        Nombre = model.Nombre,
                        Activo = model.Activo,
                        IdUserUpdate = HttpContext.Session.GetString("UserId"),
                        UpdateDate = DateTime.Now
                    });
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult UpsertChild(int? id, int? idPadre)
        {
            CatCategoryViewModel category = new CatCategoryViewModel();

            if (id == null)
            {
                category.IdPadre = idPadre;
                category.Activo = true;
                return View(category);
            }

            var cat = _unitOfWork.Category.Get(id.GetValueOrDefault());

            if (cat == null)
            {
                return NotFound();
            }

            category.Id = cat.Id;
            category.Nombre = cat.Nombre;
            category.IdPadre = cat.IdPadre;
            category.Activo = (cat.Activo != null && cat.Activo != false);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertChild(CatCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _unitOfWork.Category.Add(new CatCategorium()
                    {
                        Id = model.Id,
                        IdPadre = model.IdPadre,
                        Nombre = model.Nombre,
                        Activo = model.Activo,
                        IdUserCreate = HttpContext.Session.GetString("UserId"),
                        CreateDate = DateTime.Now
                    });
                }
                else
                {
                    _unitOfWork.Category.Update(new CatCategorium()
                    {
                        Id = model.Id,
                        IdPadre = model.IdPadre,
                        Nombre = model.Nombre,
                        Activo = model.Activo,
                        IdUserUpdate = HttpContext.Session.GetString("UserId"),
                        UpdateDate = DateTime.Now
                    });
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(AddCatChild), new { id = model.IdPadre });
            }

            return View(model);
        }

        #endregion Public Methods

        #region API_CALLS

        [HttpGet]
        public IActionResult GetAllChildCategory(int idPadre)
        {
            var parentsObj = _unitOfWork.Category.GetAll(x => x.IdPadre == idPadre);
            return Json(new { data = parentsObj });
        }

        [HttpGet]
        public IActionResult GetAllParents()
        {
            var test = _unitOfWork.SP_CALL.List<CatCategoryViewModel>(SP.Proc_GetAllCatTest, null);
            var parentsObj = _unitOfWork.Category.GetAll(x => x.IdPadre == 0);
            return Json(new { data = parentsObj });
        }

        #endregion API_CALLS
    }
}