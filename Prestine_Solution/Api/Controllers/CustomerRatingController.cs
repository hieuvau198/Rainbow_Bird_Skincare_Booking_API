using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRatingController : ControllerBase
    {
        private readonly ICustomerRatingService _ratingService;

        public CustomerRatingController(ICustomerRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerRatingDto>>> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRatingDto>> GetRatingById(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            return Ok(rating);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<CustomerRatingDto>>> GetRatingsByBookingId(int bookingId)
        {
            var ratings = await _ratingService.GetRatingsByBookingIdAsync(bookingId);
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerRatingDto>> CreateRating(CreateCustomerRatingDto createDto)
        {
            var createdRating = await _ratingService.CreateRatingAsync(createDto);
            return CreatedAtAction(nameof(GetRatingById), new { id = createdRating.RatingId }, createdRating);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRating(int id, UpdateCustomerRatingDto updateDto)
        {
            await _ratingService.UpdateRatingAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRating(int id)
        {
            await _ratingService.DeleteRatingAsync(id);
            return NoContent();
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> RatingExists(int id)
        {
            var exists = await _ratingService.RatingExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet("booking/{bookingId}/hasRating")]
        public async Task<ActionResult<bool>> HasUserRatedBooking(int bookingId)
        {
            var hasRated = await _ratingService.HasUserRatedBookingAsync(bookingId);
            return Ok(hasRated);
        }
    }
}
