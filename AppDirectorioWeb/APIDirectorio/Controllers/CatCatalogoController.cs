using DataApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIDirectorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatCatalogoController : Controller
    {
        #region Private Fields

        private readonly ICatCatalogoRepository _catCatalogoRepository;

        #endregion Private Fields

        #region Public Constructors

        public CatCatalogoController(ICatCatalogoRepository catCatalogoRepository)
        {
            _catCatalogoRepository = catCatalogoRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        
        public async Task<IActionResult> GetAllCatalogo()
        {
            return Ok(await _catCatalogoRepository.GetAllCatalogos());
        }

        [HttpGet("NombrePadre")]
       
        public async Task<IActionResult> GetCatalogosxNombrePadre(string NombrePadre)
        {
            return Ok(await _catCatalogoRepository.GetCatalogosxNombrePadre(NombrePadre));
        }

        #endregion Public Methods
    }
}