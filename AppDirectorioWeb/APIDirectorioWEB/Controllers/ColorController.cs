using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using System.Threading.Tasks;

namespace APIDirectorioWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        private readonly IColorRepository _colorRepository;
        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColor()
        {
            return Ok(await _colorRepository.GetAllColor());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColor(int id)
        {
            return Ok(await _colorRepository.GetColor(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertColor(ColorsCreateDto ColorsCreateDto)
        {
            if (ColorsCreateDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _colorRepository.InsertColor(ColorsCreateDto);

            return Created("Creado", created);
        }

        [HttpPut]
        public async Task<IActionResult> InsertColor(ColorsUpdateDto ColorsUpdateDto)
        {
            if (ColorsUpdateDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _colorRepository.UpdateColor(ColorsUpdateDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> InsertColor(int id)
        {

            await _colorRepository.DeleteColor(new ColorsDeleteDto { color_id = id });

            return NoContent();
        }



    }
}
