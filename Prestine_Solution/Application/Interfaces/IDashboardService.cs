using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDashboardService
    {
        Task<List<RevenueByDayDto>> GetDailyRevenueForWeekAsync(DateTime? startDate = null);
        Task<List<RevenueByDayDto>> GetDailyRevenueForMonthAsync(DateTime? date = null);
        Task<List<RevenueByMonthDto>> GetMonthlyRevenueForYearAsync(int? year = null);
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<List<ServiceRevenueDto>> GetTopServicesAsync(int count = 5, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<RevenueByDayDto>> GetCustomPeriodRevenueAsync(RevenueFilterDto filter);
        Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}