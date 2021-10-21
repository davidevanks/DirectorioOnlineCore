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
using Microsoft.AspNetCore.Authorization;
using ModelApp.Dto;
using ModelApp.Dto.AnuncioInfo;

using Microsoft.AspNetCore.Authorization;
using System.IO;
using AppDirectorioWeb.DATA;

namespace AppDirectorioWeb.Controllers
{
   
    public class NegociosController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDecode _decode;
        private readonly IBackendHelperApp _backend;
        private readonly ApplicationDbContext context;

        public NegociosController(ApplicationDbContext context,/*ILogger<NegociosController> logger,*/ IHttpContextAccessor httpContextAccessor, IDecode decode, IBackendHelperApp backend)
        {
            //_logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _decode = decode;
            _backend = backend;
            this.context = context;
        }
        [Utiles.CustomAttributes.Authorize(Roles.Admin, Roles.BrujulaBasica, Roles.BrujulaInicial, Roles.BrujulaPremium)]
        public async Task<IActionResult> AgregarNegocioAsync()
        {
            return View();
        }
        [Utiles.CustomAttributes.Authorize(Roles.Admin, Roles.BrujulaBasica, Roles.BrujulaInicial, Roles.BrujulaPremium)]
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<JsonResult> NegociosNuevo([FromForm] AnuncioInfoCrearDto AnuncioInfoCrearDto, List<IFormFile> files)
        {
            AnuncioInfoCrearDto.Activo = true;
            var url = $"AnuncioInfo";
            var mod = await _backend.PostAsync<int>(url, AnuncioInfoCrearDto).ConfigureAwait(false);
            // TO DO SUBIDA DE FOTOS
            //........................
            // var mod = await _backend.PostAsync<int>(url, AnuncioInfoCrearDto).ConfigureAwait(false);
            //foreach (var file in files)
            //{
            //    try
            //    {
            //        var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            //        string fecha = DateTime.Now.ToString();
            //        bool basePathExists = System.IO.Directory.Exists(basePath);
            //        if (!basePathExists) Directory.CreateDirectory(basePath);
            //        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + fecha;
            //        var extension = Path.GetExtension(file.FileName);
            //        var filePath = Path.Combine(basePath, fileName + fecha + extension);


            //        //AfiliacionCrearDto.IdentificacionByte = filePath + ";" + file.ContentType + ";" + file.FileName + ";" + extension;
            //        if (!System.IO.File.Exists(filePath))
            //        {
            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                await file.CopyToAsync(stream);
            //            }
            //            var fileModel = new FileOnFileSystemModel
            //            {
            //                CreatedOn = DateTime.UtcNow,
            //                FileType = file.ContentType,
            //                Extension = extension,
            //                Name = fileName + fecha,
            //                //Description = description,
            //                FilePath = filePath
            //            };
            //            context.FilesOnFileSystem.Add(fileModel);
            //            //context.SaveChanges();
            //        }
            //    }
            //    catch (Exception ex)
            //    {


            //    }
            //}

            //var list = new { lista = mod };
            return Json("");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetDetailByBussinesId(int id)
        {
            string url = "AnuncioInfo/api/GetAnuncioById";
            AnuncioInfoModificarDto model= new AnuncioInfoModificarDto();
            model.Id = id;
            var response =  await _backend.PostAsync<AnuncioInfoConsultarDto>(url, model);
            return View(response);
        }
    }
}
