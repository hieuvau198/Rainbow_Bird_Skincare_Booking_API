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
        private readonly IWorkingDayService _workingDayService;

        public WorkingDayController(IWorkingDayService workingDayService)
        {
            _workingDayService = workingDayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkingDayDto>>> GetAllWorkingDays()
        {
            var workingDays = await _workingDayService.GetAllWorkingDaysAsync();
            return Ok(workingDays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkingDayDto>> GetWorkingDayById(int id)
        {
            try
            {
                var workingDay = await _workingDayService.GetWorkingDayByIdAsync(id);
                return Ok(workingDay);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("byName/{dayName}")]
        public async Task<ActionResult<WorkingDayDto>> GetWorkingDayByName(string dayName)
        {
            try
            {
                var workingDay = await _workingDayService.GetWorkingDayByNameAsync(dayName);
                return Ok(workingDay);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkingDayDto>> CreateWorkingDay(CreateWorkingDayDto createDto)
        {
            try
            {
                var workingDay = await _workingDayService.CreateWorkingDayAsync(createDto);
                return CreatedAtAction(
                    nameof(GetWorkingDayById),
                    new { id = workingDay.WorkingDayId },
                    workingDay);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkingDay(int id, UpdateWorkingDayDto updateDto)
        {
            try
            {
                await _workingDayService.UpdateWorkingDayAsync(id, updateDto);
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
        public async Task<IActionResult> DeleteWorkingDay(int id)
        {
            try
            {
                await _workingDayService.DeleteWorkingDayAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
