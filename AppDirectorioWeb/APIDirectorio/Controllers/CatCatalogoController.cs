using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace APIDirectorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatCatalogoController : Controller
    {
        private readonly ICatCatalogoRepository _catCatalogoRepository;

        public CatCatalogoController(ICatCatalogoRepository catCatalogoRepository)
        {
            _catCatalogoRepository = catCatalogoRepository;
        }

        [HttpGet]
        [Route("api/GetAllCatalogo")]
        public async Task<IActionResult> GetAllCatalogo()
        {
            return Ok(await _catCatalogoRepository.GetAllCatalogos());
        }
    }
}
