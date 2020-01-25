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
    [Route("v1/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentActions _studentActions;

        public StudentsController(ILogger<StudentsController> logger, IStudentActions studentActions)
        {
            _logger = logger;
            _studentActions = studentActions;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            try
            {
                var studentDtoList = await _studentActions.GetAllAsync();

                _logger.LogInformation($"Returned all students from database.");

                return Ok(studentDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllStudents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("classes/{id}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudentsByClassId(long id)
        {
            try
            {
                var studentDtoList = await _studentActions.GetAllByClassId(id);

                _logger.LogInformation($"Returned all students from database.");

                return Ok(studentDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllStudentsByClassId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(long id)
        {
            try
            {
                var studentDto = await _studentActions.GetAsync(id);

                if (studentDto == null)
                {
                    _logger.LogError($"Student with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInformation($"Returned student with id: {id}");

                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]StudentDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("StudentDto object sent from client is null.");
                    return BadRequest("StudentDto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid StudentDto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var newId = _studentActions.Add(dto);

                return CreatedAtAction(nameof(GetStudent), new { id = newId }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(long id, [FromBody]StudentDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("StudentDto object sent from client is null.");
                    return BadRequest("StudentDto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid StudentDto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var studentDto = await _studentActions.GetAsync(id);

                if (studentDto == null)
                {
                    _logger.LogError($"Class with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _studentActions.Update(id, dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            try
            {
                var studentDto = await _studentActions.GetAsync(id);

                if (studentDto == null)
                {
                    _logger.LogError($"Student with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _studentActions.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}