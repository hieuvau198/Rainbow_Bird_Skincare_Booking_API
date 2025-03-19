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
            try
            {
                var paymentDto = await _service.CreatePaymentAsync(createDto);
                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message, ex.StackTrace) for debugging purposes if necessary
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
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

        // New endpoints for transactions

        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions([FromQuery] TransactionFilterDto filter)
        {
            // Use default values if filter is not provided
            var transactions = await _service.GetTransactionsAsync(filter ?? new TransactionFilterDto());
            return Ok(transactions);
        }

        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _service.GetTransactionByIdAsync(id);
                return Ok(transaction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }
        }

    }
}
