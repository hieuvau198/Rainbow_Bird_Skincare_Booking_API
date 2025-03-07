﻿using Application.DTOs;
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

        public TherapistProfileController(ITherapistProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<IEnumerable<TherapistProfileDto>>> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("with-reference")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<IEnumerable<TherapistProfileDto>>> GetAllProfilesWithReference()
        {
            var profiles = await _profileService.GetAllProfilesWithReferenceAsync();
            return Ok(profiles);
        }

        [HttpGet("profile/{profileId}")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> GetProfileByProfileId(int profileId)
        {
            var profile = await _profileService.GetProfileByProfileIdAsync(profileId);
            return Ok(profile);
        }

        [HttpGet("profile/{profileId}/with-reference")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> GetProfileWithReferenceByProfileId(int profileId)
        {
            var profile = await _profileService.GetProfileWithReferenceByProfileIdAsync(profileId);
            return Ok(profile);
        }

        [HttpGet("{therapistId}")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> GetProfile(int therapistId)
        {
            var profile = await _profileService.GetProfileByTherapistIdAsync(therapistId);
            return Ok(profile);
        }

        [HttpGet("{therapistId}/with-reference")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> GetProfileWithReference(int therapistId)
        {
            var profile = await _profileService.GetProfileWithReferenceByTherapistIdAsync(therapistId);
            return Ok(profile);
        }

        [HttpPost("{therapistId}")]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> CreateProfile(
            int therapistId,
            [FromForm] CreateTherapistProfileDto createDto)
        {
            var profile = await _profileService.CreateProfileAsync(therapistId, createDto);
            return CreatedAtAction(nameof(GetProfile), new { therapistId = profile.TherapistId }, profile);
        }

        [HttpPut("{therapistId}")]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<TherapistProfileDto>> UpdateProfile(
            int therapistId,
            [FromForm] UpdateTherapistProfileDto updateDto)
        {
            var profile = await _profileService.UpdateProfileAsync(therapistId, updateDto);
            return Ok(profile);
        }

        [HttpDelete("{therapistId}")]
        //[Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteProfile(int therapistId)
        {
            await _profileService.DeleteProfileAsync(therapistId);
            return NoContent();
        }
    }
}
