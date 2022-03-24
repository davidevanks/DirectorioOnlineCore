
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Controllers
{
    [Area("Catalogos")]
    public class CatalogosController : Controller
    {
       
        public CatalogosController()
        {
           
        }
       
        public async Task<JsonResult> getCatalogosByPadre(string Padre)
        {
            try
            {
                var urlCat = $"CatCatalogo/GetCatalogosxNombrePadre/"+ Padre;
               
                return Json("");
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
                
                return Json("");
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //}
    }
}
