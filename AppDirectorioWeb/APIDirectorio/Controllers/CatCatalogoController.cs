using DataApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIDirectorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
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

        [HttpGet("GetCatalogosxNombrePadre/{NombrePadre}")]
       
        public async Task<IActionResult> GetCatalogosxNombrePadre(string NombrePadre)
        {
            return Ok(await _catCatalogoRepository.GetCatalogosxNombrePadre(NombrePadre));
        }

        [HttpGet("GetCatalogosxId/{id}")]

        public async Task<IActionResult> GetCatalogosxId(int id)
        {
            return Ok(await _catCatalogoRepository.GetCatalogosxId(id));
        }

        #endregion Public Methods
    }
}