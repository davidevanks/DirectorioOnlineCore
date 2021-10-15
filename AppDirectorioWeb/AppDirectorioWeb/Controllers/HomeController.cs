using AppDirectorioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppDirectorioWeb.Utiles.CustomAttributes;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Http;

namespace AppDirectorioWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDecode _decode;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IDecode decode)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _decode = decode;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles.Admin)]
        public IActionResult Privacy()
        {
          
            ViewData["ReturnUrl"] = HttpContext.Request.Path;
            string Role = GetRole();
            return View();
        }

       
        public IActionResult Error()
        {
            ErrorViewModel e = new ErrorViewModel();
            e.MessageError = "no tienes acceso a esta página";
            return View();
        }

        private string GetRole()
        {
            if (this.HavePermission(Roles.Admin))
                return Roles.Admin;
            if (this.HavePermission(Roles.PlanMiPyme))
                return Roles.PlanMiPyme;
            if (this.HavePermission(Roles.PlanEmpresarial))
                return Roles.PlanEmpresarial;
            if (this.HavePermission(Roles.PlanTrabajadorAutonomo))
                return Roles.PlanTrabajadorAutonomo;
            return null;
        }
    }
}
