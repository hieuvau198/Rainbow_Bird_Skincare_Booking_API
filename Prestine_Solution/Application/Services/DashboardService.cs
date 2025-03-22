using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RevenueByDayDto>> GetDailyRevenueForWeekAsync(DateTime? startDate = null)
        {
            DateTime start = startDate ?? DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
            DateTime end = start.AddDays(7);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            var filteredTransactions = transactions.Where(t =>
                t.TransactionDate >= start &&
                t.TransactionDate < end);

            var result = new List<RevenueByDayDto>();

            for (int i = 0; i < 7; i++)
            {
                var day = start.AddDays(i);
                var dayTransactions = filteredTransactions.Where(t =>
                    t.TransactionDate.Date == day.Date);

                var revenue = new RevenueByDayDto
                {
                    Date = day,
                    TotalRevenue = dayTransactions.Sum(t => t.Amount),
                    TransactionCount = dayTransactions.Count(),
                    AverageTransactionAmount = dayTransactions.Any()
                        ? dayTransactions.Average(t => t.Amount)
                        : 0
                };

                result.Add(revenue);
            }

            return result;
        }

        public async Task<List<RevenueByDayDto>> GetDailyRevenueForMonthAsync(DateTime? date = null)
        {
            DateTime targetDate = date ?? DateTime.UtcNow;
            DateTime start = new DateTime(targetDate.Year, targetDate.Month, 1);
            DateTime end = start.AddMonths(1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            var filteredTransactions = transactions.Where(t =>
                t.TransactionDate >= start &&
                t.TransactionDate < end);

            var result = new List<RevenueByDayDto>();

            int daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            for (int i = 0; i < daysInMonth; i++)
            {
                var day = start.AddDays(i);
                var dayTransactions = filteredTransactions.Where(t =>
                    t.TransactionDate.Date == day.Date);

                var revenue = new RevenueByDayDto
                {
                    Date = day,
                    TotalRevenue = dayTransactions.Sum(t => t.Amount),
                    TransactionCount = dayTransactions.Count(),
                    AverageTransactionAmount = dayTransactions.Any()
                        ? dayTransactions.Average(t => t.Amount)
                        : 0
                };

                result.Add(revenue);
            }

            return result;
        }

        public async Task<List<RevenueByMonthDto>> GetMonthlyRevenueForYearAsync(int? year = null)
        {
            int targetYear = year ?? DateTime.UtcNow.Year;
            DateTime start = new DateTime(targetYear, 1, 1);
            DateTime end = start.AddYears(1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            var filteredTransactions = transactions.Where(t =>
                t.TransactionDate >= start &&
                t.TransactionDate < end);

            var result = new List<RevenueByMonthDto>();

            for (int month = 1; month <= 12; month++)
            {
                var monthStart = new DateTime(targetYear, month, 1);
                var monthEnd = monthStart.AddMonths(1);

                var monthTransactions = filteredTransactions.Where(t =>
                    t.TransactionDate >= monthStart &&
                    t.TransactionDate < monthEnd);

                var revenue = new RevenueByMonthDto
                {
                    Year = targetYear,
                    Month = month,
                    MonthName = monthStart.ToString("MMMM"),
                    TotalRevenue = monthTransactions.Sum(t => t.Amount),
                    TransactionCount = monthTransactions.Count(),
                    AverageTransactionAmount = monthTransactions.Any()
                        ? monthTransactions.Average(t => t.Amount)
                        : 0
                };

                result.Add(revenue);
            }

            return result;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var startOfYear = new DateTime(today.Year, 1, 1);
            var yesterday = today.AddDays(-1);
            var lastWeekStart = startOfWeek.AddDays(-7);
            var lastMonthStart = startOfMonth.AddMonths(-1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();

            var todayTransactions = transactions.Where(t => t.TransactionDate.Date == today);
            var thisWeekTransactions = transactions.Where(t => t.TransactionDate >= startOfWeek && t.TransactionDate < today.AddDays(1));
            var thisMonthTransactions = transactions.Where(t => t.TransactionDate >= startOfMonth && t.TransactionDate < today.AddDays(1));
            var thisYearTransactions = transactions.Where(t => t.TransactionDate >= startOfYear && t.TransactionDate < today.AddDays(1));

            var yesterdayTransactions = transactions.Where(t => t.TransactionDate.Date == yesterday);
            var lastWeekTransactions = transactions.Where(t => t.TransactionDate >= lastWeekStart && t.TransactionDate < startOfWeek);
            var lastMonthTransactions = transactions.Where(t => t.TransactionDate >= lastMonthStart && t.TransactionDate < startOfMonth);

            // Calculate growth rate compared to previous day
            decimal todayRevenue = todayTransactions.Sum(t => t.Amount);
            decimal yesterdayRevenue = yesterdayTransactions.Sum(t => t.Amount);
            decimal revenueGrowthRate = yesterdayRevenue > 0
                ? ((todayRevenue - yesterdayRevenue) / yesterdayRevenue) * 100
                : 0;

            // Get top services
            var topServicesByRevenue = thisMonthTransactions
                .GroupBy(t => new { t.ServiceId, t.ServiceName })
                .Select(g => new {
                    ServiceName = g.Key.ServiceName ?? "Unknown",
                    TotalRevenue = g.Sum(t => t.Amount)
                })
                .OrderByDescending(s => s.TotalRevenue)
                .Take(5)
                .Select(s => s.ServiceName)
                .ToList();

            // Get daily revenue for the past week
            var dailyRevenue = new List<RevenueByDayDto>();
            for (int i = 6; i >= 0; i--)
            {
                var day = today.AddDays(-i);
                var dayTransactions = transactions.Where(t => t.TransactionDate.Date == day);

                dailyRevenue.Add(new RevenueByDayDto
                {
                    Date = day,
                    TotalRevenue = dayTransactions.Sum(t => t.Amount),
                    TransactionCount = dayTransactions.Count(),
                    AverageTransactionAmount = dayTransactions.Any()
                        ? dayTransactions.Average(t => t.Amount)
                        : 0
                });
            }

            return new DashboardSummaryDto
            {
                TodayRevenue = todayRevenue,
                ThisWeekRevenue = thisWeekTransactions.Sum(t => t.Amount),
                ThisMonthRevenue = thisMonthTransactions.Sum(t => t.Amount),
                ThisYearRevenue = thisYearTransactions.Sum(t => t.Amount),
                TodayTransactionCount = todayTransactions.Count(),
                RevenueGrowthRate = revenueGrowthRate,
                DailyRevenue = dailyRevenue,
                TopServices = topServicesByRevenue
            };
        }

        public async Task<List<ServiceRevenueDto>> GetTopServicesAsync(int count = 5, DateTime? startDate = null, DateTime? endDate = null)
        {
            DateTime start = startDate ?? DateTime.UtcNow.Date.AddMonths(-1);
            DateTime end = endDate ?? DateTime.UtcNow.Date.AddDays(1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            var filteredTransactions = transactions.Where(t =>
                t.TransactionDate >= start &&
                t.TransactionDate < end);

            decimal totalRevenue = filteredTransactions.Sum(t => t.Amount);

            var serviceRevenues = filteredTransactions
                .GroupBy(t => new { t.ServiceId, t.ServiceName })
                .Select(g => new ServiceRevenueDto
                {
                    ServiceId = g.Key.ServiceId,
                    ServiceName = g.Key.ServiceName ?? "Unknown",
                    TotalRevenue = g.Sum(t => t.Amount),
                    TransactionCount = g.Count(),
                    Percentage = totalRevenue > 0
                        ? (g.Sum(t => t.Amount) / totalRevenue) * 100
                        : 0
                })
                .OrderByDescending(s => s.TotalRevenue)
                .Take(count)
                .ToList();

            return serviceRevenues;
        }

        public async Task<List<RevenueByDayDto>> GetCustomPeriodRevenueAsync(RevenueFilterDto filter)
        {
            DateTime start = filter.StartDate ?? DateTime.UtcNow.Date.AddDays(-30);
            DateTime end = filter.EndDate ?? DateTime.UtcNow.Date.AddDays(1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            var query = transactions.Where(t =>
                t.TransactionDate >= start &&
                t.TransactionDate < end);

            // Apply additional filters
            if (!string.IsNullOrWhiteSpace(filter.ServiceName))
            {
                query = query.Where(t => t.ServiceName != null &&
                    t.ServiceName.Contains(filter.ServiceName, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.ServiceId.HasValue)
            {
                query = query.Where(t => t.ServiceId == filter.ServiceId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Currency))
            {
                query = query.Where(t => t.Currency == filter.Currency);
            }

            // Group by day
            var result = query
                .GroupBy(t => t.TransactionDate.Date)
                .Select(g => new RevenueByDayDto
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(t => t.Amount),
                    TransactionCount = g.Count(),
                    AverageTransactionAmount = g.Average(t => t.Amount)
                })
                .OrderBy(r => r.Date)
                .ToList();

            return result;
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            DateTime start = startDate ?? DateTime.UtcNow.Date.AddMonths(-1);
            DateTime end = endDate ?? DateTime.UtcNow.Date.AddDays(1);

            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            return transactions
                .Where(t => t.TransactionDate >= start && t.TransactionDate < end)
                .Sum(t => t.Amount);
        }
    }
}