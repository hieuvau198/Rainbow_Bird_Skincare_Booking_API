using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetDashboardSummary()
        {
            try
            {
                var result = await _service.GetDashboardSummaryAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("revenue/week")]
        public async Task<ActionResult<IEnumerable<RevenueByDayDto>>> GetWeeklyRevenue([FromQuery] DateTime? startDate)
        {
            try
            {
                var result = await _service.GetDailyRevenueForWeekAsync(startDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("revenue/month")]
        public async Task<ActionResult<IEnumerable<RevenueByDayDto>>> GetMonthlyRevenue([FromQuery] DateTime? date)
        {
            try
            {
                var result = await _service.GetDailyRevenueForMonthAsync(date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("revenue/year")]
        public async Task<ActionResult<IEnumerable<RevenueByMonthDto>>> GetYearlyRevenue([FromQuery] int? year)
        {
            try
            {
                var result = await _service.GetMonthlyRevenueForYearAsync(year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("services/top")]
        public async Task<ActionResult<IEnumerable<ServiceRevenueDto>>> GetTopServices(
            [FromQuery] int count = 5,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var result = await _service.GetTopServicesAsync(count, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("revenue/custom")]
        public async Task<ActionResult<IEnumerable<RevenueByDayDto>>> GetCustomRevenue([FromQuery] RevenueFilterDto filter)
        {
            try
            {
                filter ??= new RevenueFilterDto();
                var result = await _service.GetCustomPeriodRevenueAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }

        [HttpGet("revenue/total")]
        public async Task<ActionResult<decimal>> GetTotalRevenue(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var result = await _service.GetTotalRevenueAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message ?? "Sorry, but something went wrong." });
            }
        }
    }
}