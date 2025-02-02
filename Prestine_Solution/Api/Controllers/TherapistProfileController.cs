using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TherapistProfileController : ControllerBase
    {
        private readonly ITherapistProfileService _profileService;
        private readonly ILogger<TherapistProfileController> _logger;

        public TherapistProfileController(
            ITherapistProfileService profileService,
            ILogger<TherapistProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<IEnumerable<TherapistProfileDto>>> GetAllProfiles()
        {
            try
            {
                var profiles = await _profileService.GetAllProfilesAsync();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all therapist profiles");
                return StatusCode(500, "An error occurred while retrieving therapist profiles");
            }
        }

        [HttpGet("{therapistId}")]
        [Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> GetProfile(int therapistId)
        {
            try
            {
                var profile = await _profileService.GetProfileByTherapistIdAsync(therapistId);
                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving therapist profile for ID: {TherapistId}", therapistId);
                return StatusCode(500, "An error occurred while retrieving the therapist profile");
            }
        }

        [HttpPost("{therapistId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> CreateProfile(
            int therapistId,
            [FromForm] CreateTherapistProfileDto createDto)
        {
            try
            {
                var profile = await _profileService.CreateProfileAsync(therapistId, createDto);
                return CreatedAtAction(
                    nameof(GetProfile),
                    new { therapistId = profile.TherapistId },
                    profile);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating therapist profile for ID: {TherapistId}", therapistId);
                return StatusCode(500, "An error occurred while creating the therapist profile");
            }
        }

        [HttpPut("{therapistId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> UpdateProfile(
            int therapistId,
            [FromForm] UpdateTherapistProfileDto updateDto)
        {
            try
            {
                var profile = await _profileService.UpdateProfileAsync(therapistId, updateDto);
                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating therapist profile for ID: {TherapistId}", therapistId);
                return StatusCode(500, "An error occurred while updating the therapist profile");
            }
        }

        [HttpDelete("{therapistId}")]
        [Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteProfile(int therapistId)
        {
            try
            {
                await _profileService.DeleteProfileAsync(therapistId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting therapist profile for ID: {TherapistId}", therapistId);
                return StatusCode(500, "An error occurred while deleting the therapist profile");
            }
        }
    }
}
