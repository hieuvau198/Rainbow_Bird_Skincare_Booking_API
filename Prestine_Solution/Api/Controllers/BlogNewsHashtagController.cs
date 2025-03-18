using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsHashtagController : ControllerBase
    {
        private readonly IBlogNewsHashtagService _blogNewsHashtagService;

        public BlogNewsHashtagController(IBlogNewsHashtagService blogNewsHashtagService)
        {
            _blogNewsHashtagService = blogNewsHashtagService;
        }

        // Blog hashtag endpoints
        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<BlogHashtagDto>>> GetBlogHashtags(int blogId)
        {
            try
            {
                var hashtags = await _blogNewsHashtagService.GetHashtagsByBlogIdAsync(blogId);
                return Ok(hashtags);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving hashtags for blog.", error = ex.Message });
            }
        }

        [HttpPost("blog")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<BlogHashtagDto>> AddHashtagToBlog(CreateBlogHashtagDto dto)
        {
            try
            {
                var blogHashtag = await _blogNewsHashtagService.AddHashtagToBlogAsync(dto);
                return Created($"api/hashtag/blog/{dto.BlogId}", blogHashtag);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error adding hashtag to blog.", error = ex.Message });
            }
        }

        [HttpDelete("blog/{blogId}/hashtag/{hashtagId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<IActionResult> RemoveHashtagFromBlog(int blogId, int hashtagId)
        {
            try
            {
                await _blogNewsHashtagService.RemoveHashtagFromBlogAsync(blogId, hashtagId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error removing hashtag from blog.", error = ex.Message });
            }
        }

        // News hashtag endpoints
        [HttpGet("news/{newsId}")]
        public async Task<ActionResult<IEnumerable<NewsHashtagDto>>> GetNewsHashtags(int newsId)
        {
            try
            {
                var hashtags = await _blogNewsHashtagService.GetHashtagsByNewsIdAsync(newsId);
                return Ok(hashtags);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving hashtags for news.", error = ex.Message });
            }
        }

        [HttpPost("news")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<NewsHashtagDto>> AddHashtagToNews(CreateNewsHashtagDto dto)
        {
            try
            {
                var newsHashtag = await _blogNewsHashtagService.AddHashtagToNewsAsync(dto);
                return Created($"api/hashtag/news/{dto.NewsId}", newsHashtag);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error adding hashtag to news.", error = ex.Message });
            }
        }

        [HttpDelete("news/{newsId}/hashtag/{hashtagId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<IActionResult> RemoveHashtagFromNews(int newsId, int hashtagId)
        {
            try
            {
                await _blogNewsHashtagService.RemoveHashtagFromNewsAsync(newsId, hashtagId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error removing hashtag from news.", error = ex.Message });
            }
        }
    }
}
