using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    public class CatalogoProductServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUpdCatConfig(int? id)
        {

            //agregar logica para agregar, guardar catalogo config. viewmodels ya listos.
            return View();
        }
    }
}
