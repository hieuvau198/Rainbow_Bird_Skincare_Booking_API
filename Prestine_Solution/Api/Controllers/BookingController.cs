using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            return Ok(await _bookingService.GetAllBookingsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            return Ok(await _bookingService.GetBookingByIdAsync(id));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByCustomerId(int customerId)
        {
            return Ok(await _bookingService.GetBookingsByCustomerIdAsync(customerId));
        }

        [HttpGet("therapist/{therapistId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByTherapistId(int therapistId)
        {
            return Ok(await _bookingService.GetBookingsByTherapistIdAsync(therapistId));
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByServiceId(int serviceId)
        {
            return Ok(await _bookingService.GetBookingsByServiceIdAsync(serviceId));
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByDate(DateOnly date)
        {
            return Ok(await _bookingService.GetBookingsByDateAsync(date));
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetBookingStatus(int id)
        {
            var statusInfo = await _bookingService.GetBookingStatusAsync(id);
            return Ok(statusInfo);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto createDto)
        {
            return Ok(await _bookingService.CreateBookingAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBooking(int id, UpdateBookingDto updateDto)
        {
            await _bookingService.UpdateBookingAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return NoContent();
        }

        // ✅ Update status using a plain string
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] string newStatus)
        {
            await _bookingService.UpdateBookingStatusAsync(id, newStatus);
            return NoContent();
        }

        // ✅ Update status using UpdateBookingDto
        [HttpPut("{id}/status/overload")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] UpdateBookingDto updateDto)
        {
            await _bookingService.UpdateBookingStatusAsync(id, updateDto);
            return NoContent();
        }
    }
}
