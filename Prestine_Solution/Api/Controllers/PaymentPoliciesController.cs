using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPoliciesController : ControllerBase
    {
        private readonly IPaymentPolicyService _paymentPolicyService;

        public PaymentPoliciesController(IPaymentPolicyService paymentPolicyService)
        {
            _paymentPolicyService = paymentPolicyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentPolicyDto>>> GetAllPaymentPolicies()
        {
            var paymentPolicies = await _paymentPolicyService.GetAllPaymentPoliciesAsync();
            return Ok(paymentPolicies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentPolicyDto>> GetPaymentPolicyById(int id)
        {
            var paymentPolicy = await _paymentPolicyService.GetPaymentPolicyByIdAsync(id);
            return Ok(paymentPolicy);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<PaymentPolicyDto>>> GetActivePaymentPolicies()
        {
            var activePolicies = await _paymentPolicyService.GetActivePaymentPoliciesAsync();
            return Ok(activePolicies);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentPolicyDto>> CreatePaymentPolicy(CreatePaymentPolicyDto createDto)
        {
            var createdPaymentPolicy = await _paymentPolicyService.CreatePaymentPolicyAsync(createDto);
            return CreatedAtAction(nameof(GetPaymentPolicyById), new { id = createdPaymentPolicy.PolicyId }, createdPaymentPolicy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentPolicy(int id, UpdatePaymentPolicyDto updateDto)
        {
            await _paymentPolicyService.UpdatePaymentPolicyAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentPolicy(int id)
        {
            await _paymentPolicyService.DeletePaymentPolicyAsync(id);
            return NoContent();
        }
    }
}
