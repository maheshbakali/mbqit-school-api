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
    [Route("v1/classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ILogger<ClassesController> _logger;
        private readonly IClassActions _classActions;

        public ClassesController(ILogger<ClassesController> logger, IClassActions classActions)
        {
            _logger = logger;
            _classActions = classActions;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAllClasses()
        {
            try
            {
                var classDtoList = await _classActions.GetAllAsync();

                _logger.LogInformation($"Returned all classes from database.");

                return Ok(classDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClasses action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(long id)
        {
            try
            {
                var classDto = await _classActions.GetAsync(id);

                if (classDto == null)
                {
                    _logger.LogError($"Class with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInformation($"Returned class with id: {id}");

                return Ok(classDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClass action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public IActionResult CreateClass([FromBody]ClassDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("ClassDto object sent from client is null.");
                    return BadRequest("ClassDto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ClassDto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var newId = _classActions.Add(dto);
                dto.ClassId = newId;
                
                return CreatedAtAction(nameof(GetClass), new { id = newId }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateClass action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(long id, [FromBody]ClassDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("ClassDto object sent from client is null.");
                    return BadRequest("ClassDto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ClassDto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var classDto = await _classActions.GetAsync(id);

                if (classDto == null)
                {
                    _logger.LogError($"Class with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _classActions.Update(id, dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateClass action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(long id)
        {
            try
            {
                var classDto = await _classActions.GetAsync(id);

                if (classDto == null)
                {
                    _logger.LogError($"Class with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _classActions.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteClass action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

 }