using DataApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelApp.Dto.FotosAnuncio;
using System.Threading.Tasks;

namespace APIDirectorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class FotosAnuncioController : Controller
    {
        #region Private Fields

        private readonly IFotosAnuncioRepository _FotosAnuncioRepository;

        #endregion Private Fields

        #region Public Constructors

        public FotosAnuncioController(IFotosAnuncioRepository FotosAnuncioRepository)
        {
            _FotosAnuncioRepository = FotosAnuncioRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("GetAllFotosAnuncio/{IdNegocio}")]
        public async Task<IActionResult> GetAllFotosAnuncio(int IdNegocio)
        {
            return Ok(await _FotosAnuncioRepository.GetAllFotosAnuncio(IdNegocio));
        }


        [HttpGet("GetByIdFotosAnuncio/{id}")]
        public async Task<IActionResult> GetByIdFotosAnuncio(int id)
        {
            return Ok(await _FotosAnuncioRepository.GetByIdFotosAnuncio(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertFotosAnuncio(FotosAnuncioCrearDto model)
        {
            return Ok(await _FotosAnuncioRepository.InsertFotosAnuncio(model));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFotosAnuncio(FotosAnuncioModificarDto model)
        {
            return Ok(await _FotosAnuncioRepository.UpdateFotosAnuncio(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFotosAnuncio(int id)
        {
            return Ok(await _FotosAnuncioRepository.DeleteFotosAnuncio(id));
        }

        #endregion Public Methods
    }
}