using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerFeedbackController : ControllerBase
    {
        private readonly ICustomerFeedbackService _feedbackService;

        public CustomerFeedbackController(ICustomerFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerFeedbackDto>>> GetAllFeedbacks()
        {
            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerFeedbackDto>> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            return Ok(feedback);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<CustomerFeedbackDto>>> GetFeedbacksByBookingId(int bookingId)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByBookingIdAsync(bookingId);
            return Ok(feedbacks);
        }

        [HttpGet("recent/{count}")]
        public async Task<ActionResult<IEnumerable<CustomerFeedbackDto>>> GetRecentFeedbacks(int count)
        {
            var feedbacks = await _feedbackService.GetRecentFeedbacksAsync(count);
            return Ok(feedbacks);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerFeedbackDto>> CreateFeedback(CreateCustomerFeedbackDto createDto)
        {
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(createDto);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.CustomerFeedbackId }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeedback(int id, UpdateCustomerFeedbackDto updateDto)
        {
            await _feedbackService.UpdateFeedbackAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedback(int id)
        {
            await _feedbackService.DeleteFeedbackAsync(id);
            return NoContent();
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> FeedbackExists(int id)
        {
            var exists = await _feedbackService.FeedbackExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet("booking/{bookingId}/hasFeedback")]
        public async Task<ActionResult<bool>> HasBookingFeedback(int bookingId)
        {
            var hasFeedback = await _feedbackService.HasBookingFeedbackAsync(bookingId);
            return Ok(hasFeedback);
        }
    }
}