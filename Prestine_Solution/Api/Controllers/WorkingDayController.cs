using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkingDayController : ControllerBase
    {
        private readonly IWorkingDayService _service;

        public WorkingDayController(IWorkingDayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workingDays = await _service.GetAllWorkingDaysAsync();
            return Ok(workingDays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var workingDay = await _service.GetWorkingDayByIdAsync(id);
                return Ok(workingDay);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("byName/{dayName}")]
        public async Task<IActionResult> GetByName(string dayName)
        {
            try
            {
                var workingDay = await _service.GetWorkingDayByNameAsync(dayName);
                return Ok(workingDay);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkingDayDto createDto)
        {
            try
            {
                var workingDay = await _service.CreateWorkingDayAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = workingDay.WorkingDayId }, workingDay);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkingDayDto updateDto)
        {
            try
            {
                await _service.UpdateWorkingDayAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteWorkingDayAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
