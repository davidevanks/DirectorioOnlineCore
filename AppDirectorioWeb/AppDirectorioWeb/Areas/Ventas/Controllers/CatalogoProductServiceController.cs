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
    }
}
