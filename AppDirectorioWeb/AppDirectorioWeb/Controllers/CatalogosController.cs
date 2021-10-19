using AppDirectorioWeb.RequestProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly IBackendHelperApp _backend;
        public CatalogosController(IBackendHelperApp backend)
        {
            _backend = backend;
        }
       
        public async Task<JsonResult> getCatalogosByPadre(string Padre)
        {
            try
            {
                var urlCat = $"CatCatalogo/GetCatalogosxNombrePadre/"+ Padre;
                var modeloCAt = await _backend.GetAsync<List<CatCatalogoRequest>>(urlCat).ConfigureAwait(false) ?? new List<CatCatalogoRequest>();
                return Json(new SelectList(modeloCAt, "Id", "Nombre"));
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<JsonResult> GetCatalogosxId(int id)
        {
            try
            {
                var urlCat = $"CatCatalogo/GetCatalogosxId/" + id;
                var modeloCAt = await _backend.GetAsync<List<CatCatalogoRequest>>(urlCat).ConfigureAwait(false) ?? new List<CatCatalogoRequest>();
                return Json(new SelectList(modeloCAt, "Id", "Nombre"));
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //}
    }
}
