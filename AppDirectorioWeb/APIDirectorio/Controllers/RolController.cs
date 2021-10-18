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
    public class RolController : ControllerBase
    {
        private readonly IManageRolRepository _manageRolRepository;

        public RolController(IManageRolRepository manageRolRepository)
        {
            _manageRolRepository = manageRolRepository;
        }


        [HttpGet]
        [Route("api/GetPlans")]
        public async Task<IActionResult> GetPlans()
        {
            return Ok(await _manageRolRepository.GetListPlan());
        }
    }
}
