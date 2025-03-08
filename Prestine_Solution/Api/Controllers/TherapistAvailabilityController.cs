using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            try
            {
                var availabilities = await _availabilityService.GetAllAvailabilitiesAsync();
                return Ok(availabilities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("therapist/{therapistId}")]
        public async Task<ActionResult<IEnumerable<TherapistAvailabilityDto>>> GetTherapistAvailabilities(
            int therapistId,
            [FromQuery] DateOnly? date = null)
        {
            try
            {
                var availabilities = await _availabilityService.GetTherapistAvailabilitiesAsync(therapistId, date);
                return Ok(availabilities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("slot/{slotId}")]
        public async Task<ActionResult<IEnumerable<TherapistAvailabilityDto>>> GetAvailabilitiesBySlotId(
            int slotId,
            [FromQuery] DateOnly? date = null)
        {
            try
            {
                var availabilities = await _availabilityService.GetAvailabilitiesBySlotIdAsync(slotId, date);
                return Ok(availabilities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TherapistAvailabilityDto>> CreateAvailability(
            [FromBody] CreateTherapistAvailabilityDto createDto)
        {
            try
            {
                var createdAvailability = await _availabilityService.CreateAvailabilityAsync(createDto);
                return CreatedAtAction(
                    nameof(GetAvailabilityById),
                    new { id = createdAvailability.AvailabilityId },
                    createdAvailability);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailability(
            int id,
            [FromBody] UpdateTherapistAvailabilityDto updateDto)
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}