using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.BLL.Actions;
using School.BLL.Dto;

namespace School.API.Controllers
{
    [Route("v1/salutations")]
    [ApiController]
    public class SalutationsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly ISalutationActions _salutationActions;

        public SalutationsController(ILogger<StudentsController> logger, ISalutationActions salutationActions)
        {
            _logger = logger;
            _salutationActions = salutationActions;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<SalutationDto>>> GetAllSalutations()
        {
            try
            {
                var salutationDtoList = await _salutationActions.GetAllAsync();

                _logger.LogInformation($"Returned all salutations from database.");

                return Ok(salutationDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSalutations action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}