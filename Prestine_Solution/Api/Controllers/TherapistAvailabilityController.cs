using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TherapistAvailabilityController : ControllerBase
    {
        private readonly ITherapistAvailabilityService _availabilityService;

        public TherapistAvailabilityController(ITherapistAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TherapistAvailabilityDto>>> GetAllAvailabilities()
        {
            var availabilities = await _availabilityService.GetAllAvailabilitiesAsync();
            return Ok(availabilities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TherapistAvailabilityDto>> GetAvailabilityById(int id)
        {
            try
            {
                var availability = await _availabilityService.GetAvailabilityByIdAsync(id);
                return Ok(availability);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("therapist/{therapistId}")]
        public async Task<ActionResult<IEnumerable<TherapistAvailabilityDto>>> GetTherapistAvailabilities(
            int therapistId,
            [FromQuery] DateOnly? date = null)
        {
            var availabilities = await _availabilityService.GetTherapistAvailabilitiesAsync(therapistId, date);
            return Ok(availabilities);
        }

        [HttpPost]
        public async Task<ActionResult<TherapistAvailabilityDto>> CreateAvailability(CreateTherapistAvailabilityDto createDto)
        {
            try
            {
                var availability = await _availabilityService.CreateAvailabilityAsync(createDto);
                return CreatedAtAction(
                    nameof(GetAvailabilityById),
                    new { id = availability.AvailabilityId },
                    availability);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailability(int id, UpdateTherapistAvailabilityDto updateDto)
        {
            try
            {
                await _availabilityService.UpdateAvailabilityAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            try
            {
                await _availabilityService.DeleteAvailabilityAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
