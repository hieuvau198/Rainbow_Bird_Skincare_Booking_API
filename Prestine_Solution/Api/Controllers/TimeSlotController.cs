using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetAllTimeSlots()
        {
            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlotDto>> GetTimeSlotById(int id)
        {
            try
            {
                var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);
                return Ok(timeSlot);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("workingDay/{workingDayId}")]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetTimeSlotsByWorkingDay(int workingDayId)
        {
            var timeSlots = await _timeSlotService.GetTimeSlotsByWorkingDayAsync(workingDayId);
            return Ok(timeSlots);
        }

        [HttpPost]
        public async Task<ActionResult<TimeSlotDto>> CreateTimeSlot(CreateTimeSlotDto createDto)
        {
            try
            {
                var timeSlot = await _timeSlotService.CreateTimeSlotAsync(createDto);
                return CreatedAtAction(
                    nameof(GetTimeSlotById),
                    new { id = timeSlot.SlotId },
                    timeSlot);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(int id, UpdateTimeSlotDto updateDto)
        {
            try
            {
                await _timeSlotService.UpdateTimeSlotAsync(id, updateDto);
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
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            try
            {
                await _timeSlotService.DeleteTimeSlotAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
