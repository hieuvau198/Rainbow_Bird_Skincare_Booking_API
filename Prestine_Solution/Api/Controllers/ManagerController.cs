using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerDto>>> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerDto>> GetManager(int id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);
            return Ok(manager);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<ManagerDto>> GetManagerByUserId(int userId)
        {
            var manager = await _managerService.GetManagerByUserIdAsync(userId);
            return Ok(manager);
        }

        [HttpPost]
        public async Task<ActionResult<ManagerDto>> CreateManager(CreateManagerDto createManagerDto)
        {
            var manager = await _managerService.CreateManagerAsync(createManagerDto);
            return CreatedAtAction(nameof(GetManager), new { id = manager.ManagerId }, manager);
        }

        [HttpPost("with-user")]
        public async Task<ActionResult<ManagerDto>> CreateManagerWithUser(CreateManagerUserDto createManagerUserDto)
        {
            var manager = await _managerService.CreateManagerWithUserAsync(createManagerUserDto);
            return CreatedAtAction(nameof(GetManager), new { id = manager.ManagerId }, manager);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, UpdateManagerDto updateManagerDto)
        {
            await _managerService.UpdateManagerAsync(id, updateManagerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            await _managerService.DeleteManagerAsync(id);
            return NoContent();
        }
    }
}
