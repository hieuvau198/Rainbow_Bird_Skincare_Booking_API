using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashtagController : ControllerBase
    {
        private readonly IHashtagService _hashtagService;

        public HashtagController(IHashtagService hashtagService)
        {
            _hashtagService = hashtagService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HashtagDto>>> GetAllHashtags()
        {
            try
            {
                var hashtags = await _hashtagService.GetAllHashtagsAsync();
                return Ok(hashtags);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving hashtags.", error = ex.Message });
            }
        }

        [HttpGet("{hashtagId}")]
        public async Task<ActionResult<HashtagDto>> GetHashtag(int hashtagId)
        {
            try
            {
                var hashtag = await _hashtagService.GetHashtagByIdAsync(hashtagId);
                return Ok(hashtag);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving hashtag.", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<HashtagDto>> CreateHashtag([FromBody] CreateHashtagDto createDto)
        {
            try
            {
                var hashtag = await _hashtagService.CreateHashtagAsync(createDto);
                return CreatedAtAction(nameof(GetHashtag), new { hashtagId = hashtag.HashtagId }, hashtag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error creating hashtag.", error = ex.Message });
            }
        }

        [HttpPut("{hashtagId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<HashtagDto>> UpdateHashtag(int hashtagId, [FromBody] UpdateHashtagDto updateDto)
        {
            try
            {
                var hashtag = await _hashtagService.UpdateHashtagAsync(hashtagId, updateDto);
                return Ok(hashtag);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error updating hashtag.", error = ex.Message });
            }
        }

        [HttpDelete("{hashtagId}")]
        [Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteHashtag(int hashtagId)
        {
            try
            {
                await _hashtagService.DeleteHashtagAsync(hashtagId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error deleting hashtag.", error = ex.Message });
            }
        }
    }
}