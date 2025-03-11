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
            try
            {
                var statusInfo = await _bookingService.GetBookingStatusAsync(id);
                return Ok(new { success = true, data = statusInfo });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An unexpected error occurred. Please try again." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createDto)
        {
            try
            {
                var booking = await _bookingService.CreateBookingAsync(createDto);
                return Ok(new { success = true, message = "Your booking has been successfully created.", data = booking });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Something went wrong while creating your booking. Please try again." });
            }
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

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                await _bookingService.UpdateBookingStatusAsync(id, newStatus);
                return Ok(new { success = true, message = "Booking status updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Something went wrong while updating the booking status. Please try again." });
            }
        }

        // ✅ Update status using UpdateBookingDto
        [HttpPut("{id}/status/overload")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] UpdateBookingDto updateDto)
        {
            await _bookingService.UpdateBookingStatusAsync(id, updateDto);
            return NoContent();
        }

        [HttpPatch("{id}/therapist")]
        public async Task<IActionResult> UpdateTherapist(int id, [FromBody] UpdateBookingTherapistDto dto)
        {
            if (dto == null || dto.TherapistId <= 0)
                return BadRequest(new { success = false, message = "Invalid therapist ID." });

            try
            {
                await _bookingService.UpdateBookingTherapistAsync(id, dto.TherapistId);
                return Ok(new { success = true, message = "Therapist updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Something went wrong while updating the therapist. Please try again." });
            }
        }
    }
}
