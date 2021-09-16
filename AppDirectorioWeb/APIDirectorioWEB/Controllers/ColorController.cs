using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
