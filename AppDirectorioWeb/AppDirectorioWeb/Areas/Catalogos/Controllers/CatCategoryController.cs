using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;

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
