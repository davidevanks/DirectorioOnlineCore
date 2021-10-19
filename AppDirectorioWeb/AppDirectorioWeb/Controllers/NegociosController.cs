using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDirectorioWeb.Utiles.CustomAttributes;
using AppDirectorioWeb.Utiles.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AppDirectorioWeb.Models;
using AppDirectorioWeb.RequestProvider.Interfaces;
using ModelApp.Dto;
using ModelApp.Dto.AnuncioInfo;
using Microsoft.AspNetCore.Authorization;

namespace AppDirectorioWeb.Controllers
{
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDecode _decode;
        private readonly IBackendHelperApp _backend;
   

        public NegociosController(/*ILogger<NegociosController> logger,*/ IHttpContextAccessor httpContextAccessor, IDecode decode, IBackendHelperApp backend)
        {
            //_logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _decode = decode;
            _backend = backend;
        }

        public async Task<IActionResult> AgregarNegocioAsync()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<JsonResult> NegociosNuevo([FromForm] AnuncioInfoCrearDto AnuncioInfoCrearDto)
        {
            AnuncioInfoCrearDto.Activo = true;
            var url = $"AnuncioInfo";
            var mod = await _backend.PostAsync<AnuncioInfoCrearDto>(url, AnuncioInfoCrearDto).ConfigureAwait(false);

            var list = new { lista = mod };
            return Json(list);
        }
    }
}
