using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelBookingsController : ControllerBase
    {
        private readonly ICancelBookingService _cancelBookingService;

        public CancelBookingsController(ICancelBookingService cancelBookingService)
        {
            _cancelBookingService = cancelBookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancelBookingDto>>> GetAllCancelBookings()
        {
            var cancelBookings = await _cancelBookingService.GetAllCancelBookingsAsync();
            return Ok(cancelBookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancelBookingDto>> GetCancelBookingById(int id)
        {
            var cancelBooking = await _cancelBookingService.GetCancelBookingByIdAsync(id);
            return Ok(cancelBooking);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<CancelBookingDto>> GetCancelBookingByBookingId(int bookingId)
        {
            var cancelBooking = await _cancelBookingService.GetCancelBookingByBookingIdAsync(bookingId);
            return Ok(cancelBooking);
        }

        [HttpPost]
        public async Task<ActionResult<CancelBookingDto>> CreateCancelBooking(CreateCancelBookingDto createDto)
        {
            var createdCancelBooking = await _cancelBookingService.CreateCancelBookingAsync(createDto);
            return CreatedAtAction(nameof(GetCancelBookingById), new { id = createdCancelBooking.CancellationId }, createdCancelBooking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCancelBooking(int id, UpdateCancelBookingDto updateDto)
        {
            await _cancelBookingService.UpdateCancelBookingAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelBooking(int id)
        {
            await _cancelBookingService.DeleteCancelBookingAsync(id);
            return NoContent();
        }
    }
}
