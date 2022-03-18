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
using AppDirectorioWeb.DATA;

namespace AppDirectorioWeb.Controllers
{
   
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly IHttpContextAccessor _httpContextAccessor;
     
        private readonly ApplicationDbContext context;

        public NegociosController(ApplicationDbContext context,/*ILogger<NegociosController> logger,*/ IHttpContextAccessor httpContextAccessor)
        {
        
        }
      
        public async Task<IActionResult> AgregarNegocioAsync()
        {
            return View();
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
