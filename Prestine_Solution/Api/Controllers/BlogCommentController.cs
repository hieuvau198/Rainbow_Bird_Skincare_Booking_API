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
    public class BlogCommentController : ControllerBase
    {
        private readonly IBlogCommentService _commentService;

        public BlogCommentController(IBlogCommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<BlogCommentDto>>> GetCommentsForBlog(int blogId)
        {
            try
            {
                var comments = await _commentService.GetAllCommentsForBlogAsync(blogId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving comments.", error = ex.Message });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetComments(
            [FromQuery] int? blogId = null,
            [FromQuery] int? parentCommentId = null,
            [FromQuery] string sortBy = "createdAt",
            [FromQuery] string order = "desc",
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var comments = await _commentService.GetCommentsAsync(blogId, parentCommentId, sortBy, order, page, size);
                return Ok(new
                {
                    totalItems = comments.Count(),
                    currentPage = page,
                    pageSize = size,
                    data = comments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving comments.", error = ex.Message });
            }
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<BlogCommentDto>> GetComment(int commentId)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(commentId);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving comment.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BlogCommentDto>> CreateComment([FromBody] CreateBlogCommentDto createDto)
        {
            try
            {
                var comment = await _commentService.CreateCommentAsync(createDto);
                return CreatedAtAction(nameof(GetComment), new { commentId = comment.CommentId }, comment);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error creating comment.", error = ex.Message });
            }
        }

        [HttpPut("{commentId}")]
        public async Task<ActionResult<BlogCommentDto>> UpdateComment(int commentId, [FromForm] UpdateBlogCommentDto updateDto)
        {
            try
            {
                var comment = await _commentService.UpdateCommentAsync(commentId, updateDto);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error updating comment.", error = ex.Message });
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error deleting comment.", error = ex.Message });
            }
        }

        [HttpPost("{commentId}/upvote")]
        public async Task<ActionResult<BlogCommentDto>> UpvoteComment(int commentId)
        {
            try
            {
                var comment = await _commentService.UpvoteCommentAsync(commentId);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error upvoting comment.", error = ex.Message });
            }
        }

        [HttpPost("{commentId}/downvote")]
        public async Task<ActionResult<BlogCommentDto>> DownvoteComment(int commentId)
        {
            try
            {
                var comment = await _commentService.DownvoteCommentAsync(commentId);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error downvoting comment.", error = ex.Message });
            }
        }
    }
}