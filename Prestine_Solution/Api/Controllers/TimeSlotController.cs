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
        private readonly ITimeSlotService _service;

        public TimeSlotController(ITimeSlotService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeSlots = await _service.GetAllTimeSlotsAsync();
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var timeSlot = await _service.GetTimeSlotByIdAsync(id);
                return Ok(timeSlot);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("workingDay/{workingDayId}")]
        public async Task<IActionResult> GetByWorkingDay(int workingDayId)
        {
            var timeSlots = await _service.GetTimeSlotsByWorkingDayAsync(workingDayId);
            return Ok(timeSlots);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimeSlotDto createDto)
        {
            try
            {
                var timeSlot = await _service.CreateTimeSlotAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = timeSlot.SlotId }, timeSlot);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTimeSlotDto updateDto)
        {
            try
            {
                await _service.UpdateTimeSlotAsync(id, updateDto);
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
                await _service.DeleteTimeSlotAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
