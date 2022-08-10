using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Cuponera.Controllers
{
    [Area("Cuponera")]
    public class CuponeraController : Controller
    {
        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult Index()
        {
            int 
            //hacer logica para traer los cupones, lgica para bussinesAdmin y Admin site
            
            return View();
        }

        [Authorize(Roles = SP.Role_BusinesAdmin)]
        public IActionResult Add()
        {

            return View();
        }
    }
}
