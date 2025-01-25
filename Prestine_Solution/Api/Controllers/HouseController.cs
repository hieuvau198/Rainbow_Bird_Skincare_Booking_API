using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        public HouseController(IHouseService houseService)
        {  
            _houseService = houseService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var houses = await _houseService.GetAllAsync();
            return Ok(houses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(HouseDto houseDto)
        {
            await _houseService.AddAsync(houseDto);
            return CreatedAtAction(nameof(GetAll), null);
        }
    }
}
