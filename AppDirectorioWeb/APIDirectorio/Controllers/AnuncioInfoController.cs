using DataApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelApp.Dto.AnuncioInfo;
using System.Threading.Tasks;

namespace APIDirectorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AnuncioInfoController : Controller
    {
        #region Private Fields

        private readonly IAnuncioInfoRepository _AnuncioInfoRepository;

        #endregion Private Fields

        #region Public Constructors

        public AnuncioInfoController(IAnuncioInfoRepository AnuncioInfoRepository)
        {
            _AnuncioInfoRepository = AnuncioInfoRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogo()
        {
            return Ok(await _AnuncioInfoRepository.GetAllAnuncioInfo());
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAllCatalogo(int id)
        //{
        //    return Ok(await _AnuncioInfoRepository.get(id));
        //}

        [HttpPost]
        public async Task<IActionResult> InsertAnuncioInfo(AnuncioInfoCrearDto model)
        {
            return Ok(await _AnuncioInfoRepository.InsertAnuncioInfo(model));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnuncioInfo(AnuncioInfoModificarDto model)
        {
            return Ok(await _AnuncioInfoRepository.UpdateAnuncioInfo(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnuncioInfo(int id)
        {
            return Ok(await _AnuncioInfoRepository.DeleteAnuncioInfo(id));
        }

        [HttpPost]
        [Route("api/GetAllAnuncioBySearch")]
        public async Task<IActionResult> GetAllAnuncioBySearch(SearchBussinesRequest model)
        {
            return Ok(await _AnuncioInfoRepository.GetAllAnuncioBySearch(model));
        }

        [HttpPost]
        [Route("api/GetAnuncioById")]
        public async Task<IActionResult> GetAnuncioById(AnuncioInfoModificarDto model)
        {
            return Ok(await _AnuncioInfoRepository.GetByIdAnuncioInfo(model));
        }

        #endregion Public Methods
    }
}