using AppDirectorioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Authorization;
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
       
        public IActionResult Privacy()
        {
            string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            var tokenValue = _decode.DecodeToken(token);
            var Roles = tokenValue.Claims.Where(x => x.Type == "RoleName").Select(x=>x.Value).ToList();

            if (!String.IsNullOrEmpty(token))
            {
                if (Roles.Contains("Admin"))
                {
                    return View();
                }
                else
                {
                    ErrorViewModel e = new ErrorViewModel();
                    e.MessageError = "no tienes acceso a esta página";
                    return View("Error", e);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
