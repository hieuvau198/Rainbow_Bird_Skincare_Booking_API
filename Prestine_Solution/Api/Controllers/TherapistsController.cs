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
            var therapists = await _therapistService.GetTherapistsAsync();
            return Ok(therapists);
        }

        [HttpGet("with-reference")]
        public async Task<ActionResult<IEnumerable<TherapistDto>>> GetAllTherapistsWithReference()
        {
            var therapists = await _therapistService.GetTherapistsWithReferenceAsync();
            return Ok(therapists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TherapistDto>> GetTherapist(int id)
        {
            var therapist = await _therapistService.GetByIdAsync(id);
            return Ok(therapist);
        }

        [HttpGet("with-reference/{id}")]
        public async Task<ActionResult<TherapistDto>> GetTherapistWithReference(int id)
        {
            var therapist = await _therapistService.GetByIdWithReferenceAsync(id);
            return Ok(therapist);
        }

        [HttpPost]
        public async Task<ActionResult<TherapistDto>> CreateTherapist(CreateTherapistDto createTherapistDto)
        {
            var therapist = await _therapistService.CreateTherapistAsync(createTherapistDto);
            return CreatedAtAction(nameof(GetTherapist), new { id = therapist.TherapistId }, therapist);
        }

        [HttpPost("with-user")]
        public async Task<ActionResult<TherapistDto>> CreateTherapistWithUser(CreateTherapistUserDto createTherapistUserDto)
        {
            var therapist = await _therapistService.CreateTherapistWithUserAsync(createTherapistUserDto);
            return CreatedAtAction(nameof(GetTherapist), new { id = therapist.TherapistId }, therapist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTherapist(int id, UpdateTherapistDto updateTherapistDto)
        {
            await _therapistService.UpdateTherapistAsync(id, updateTherapistDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTherapist(int id)
        {
            await _therapistService.DeleteTherapistAsync(id);
            return NoContent();
        }
    }
}
