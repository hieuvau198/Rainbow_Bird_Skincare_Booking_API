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
        private readonly ITherapistAvailabilityService _service;

        public TherapistAvailabilityController(ITherapistAvailabilityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var availabilities = await _service.GetAllAvailabilitiesAsync();
            return Ok(availabilities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var availability = await _service.GetAvailabilityByIdAsync(id);
                return Ok(availability);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("therapist/{therapistId}")]
        public async Task<IActionResult> GetByTherapist(int therapistId, [FromQuery] DateOnly? date = null)
        {
            var availabilities = await _service.GetTherapistAvailabilitiesAsync(therapistId, date);
            return Ok(availabilities);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTherapistAvailabilityDto createDto)
        {
            try
            {
                var availability = await _service.CreateAvailabilityAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = availability.AvailabilityId }, availability);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTherapistAvailabilityDto updateDto)
        {
            try
            {
                await _service.UpdateAvailabilityAsync(id, updateDto);
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
                await _service.DeleteAvailabilityAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
