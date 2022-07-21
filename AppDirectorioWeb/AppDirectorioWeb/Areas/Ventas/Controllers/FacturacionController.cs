using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Utiles;

namespace AppDirectorioWeb.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    public class FacturacionController : Controller
    {
        private readonly DirectorioOnlineCoreContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public FacturacionController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DirectorioOnlineCoreContext db)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        // GET: FacturacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FacturacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #region API_CALLS
        [HttpGet]
        public IActionResult GetInvoices(string userId)
        {
            List<FacturaViewModel> invoiceList = new List<FacturaViewModel>();
            invoiceList = _unitOfWork.Factura.GetInvoice("");
            return Json(new { data = invoiceList });
        }
        #endregion

    }
}
