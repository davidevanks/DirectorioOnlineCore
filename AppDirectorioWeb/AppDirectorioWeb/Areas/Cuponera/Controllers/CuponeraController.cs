using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Areas.Cuponera.Controllers
{
    [Area("Cuponera")]
    public class CuponeraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
