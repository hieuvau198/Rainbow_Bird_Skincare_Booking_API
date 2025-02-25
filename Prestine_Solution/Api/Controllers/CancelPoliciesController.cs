using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelPoliciesController : ControllerBase
    {
        private readonly ICancelPolicyService _cancelPolicyService;

        public CancelPoliciesController(ICancelPolicyService cancelPolicyService)
        {
            _cancelPolicyService = cancelPolicyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancelPolicyDto>>> GetAllCancelPolicies()
        {
            var cancelPolicies = await _cancelPolicyService.GetAllCancelPoliciesAsync();
            return Ok(cancelPolicies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancelPolicyDto>> GetCancelPolicyById(int id)
        {
            var cancelPolicy = await _cancelPolicyService.GetCancelPolicyByIdAsync(id);
            return Ok(cancelPolicy);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CancelPolicyDto>>> GetActiveCancelPolicies()
        {
            var activePolicies = await _cancelPolicyService.GetActiveCancelPoliciesAsync();
            return Ok(activePolicies);
        }

        [HttpPost]
        public async Task<ActionResult<CancelPolicyDto>> CreateCancelPolicy(CreateCancelPolicyDto createDto)
        {
            var createdCancelPolicy = await _cancelPolicyService.CreateCancelPolicyAsync(createDto);
            return CreatedAtAction(nameof(GetCancelPolicyById), new { id = createdCancelPolicy.PolicyId }, createdCancelPolicy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCancelPolicy(int id, UpdateCancelPolicyDto updateDto)
        {
            await _cancelPolicyService.UpdateCancelPolicyAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelPolicy(int id)
        {
            await _cancelPolicyService.DeleteCancelPolicyAsync(id);
            return NoContent();
        }
    }
}
