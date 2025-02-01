using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TherapistsController : ControllerBase
    {
        private readonly ITherapistService _therapistService;

        public TherapistsController(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TherapistDto>>> GetAllTherapists()
        {
            var therapists = await _therapistService.GetAllTherapistsAsync();
            return Ok(therapists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TherapistDto>> GetTherapist(int id)
        {
            try
            {
                var therapist = await _therapistService.GetTherapistByIdAsync(id);
                return Ok(therapist);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TherapistDto>> CreateTherapist(CreateTherapistDto createTherapistDto)
        {
            try
            {
                var therapist = await _therapistService.CreateTherapistAsync(createTherapistDto);
                return CreatedAtAction(nameof(GetTherapist), new { id = therapist.TherapistId }, therapist);
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
        public async Task<IActionResult> UpdateTherapist(int id, UpdateTherapistDto updateTherapistDto)
        {
            try
            {
                await _therapistService.UpdateTherapistAsync(id, updateTherapistDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTherapist(int id)
        {
            try
            {
                await _therapistService.DeleteTherapistAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
