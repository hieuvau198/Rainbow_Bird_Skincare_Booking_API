using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            return Ok(await _service.GetAllPaymentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPaymentById(int id)
        {
            return Ok(await _service.GetPaymentByIdAsync(id));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByStatus(string status)
        {
            return Ok(await _service.GetPaymentsByStatusAsync(status));
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            return Ok(await _service.GetPaymentsByDateRangeAsync(startDate, endDate));
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto createDto)
        {
            return Ok(await _service.CreatePaymentAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(int id, UpdatePaymentDto updateDto)
        {
            await _service.UpdatePaymentAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            await _service.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
