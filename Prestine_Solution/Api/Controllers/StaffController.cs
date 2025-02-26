using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAllStaff()
        {
            var staff = await _staffService.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaff(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            return Ok(staff);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<StaffDto>> GetStaffByUserId(int userId)
        {
            var staff = await _staffService.GetStaffByUserIdAsync(userId);
            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff(CreateStaffDto createStaffDto)
        {
            var staff = await _staffService.CreateStaffAsync(createStaffDto);
            return CreatedAtAction(nameof(GetStaff), new { id = staff.StaffId }, staff);
        }

        [HttpPost("with-user")]
        public async Task<ActionResult<StaffDto>> CreateStaffWithUser(CreateStaffUserDto createStaffUserDto)
        {
            var staff = await _staffService.CreateStaffWithUserAsync(createStaffUserDto);
            return CreatedAtAction(nameof(GetStaff), new { id = staff.StaffId }, staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, UpdateStaffDto updateStaffDto)
        {
            await _staffService.UpdateStaffAsync(id, updateStaffDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
