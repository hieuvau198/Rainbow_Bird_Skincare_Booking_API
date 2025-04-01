using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class RevenueByDayDto
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageTransactionAmount { get; set; }
    }

    public class RevenueByMonthDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageTransactionAmount { get; set; }
    }

    public class RevenueByYearDto
    {
        public int Year { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageTransactionAmount { get; set; }
    }

    public class DashboardSummaryDto
    {
        public decimal TodayRevenue { get; set; }
        public decimal ThisWeekRevenue { get; set; }
        public decimal ThisMonthRevenue { get; set; }
        public decimal ThisYearRevenue { get; set; }
        public int TodayTransactionCount { get; set; }
        public decimal RevenueGrowthRate { get; set; } // Percentage compared to previous period
        public List<RevenueByDayDto> DailyRevenue { get; set; }
        public List<string> TopServices { get; set; }
    }

    public class RevenueFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ServiceName { get; set; }
        public int? ServiceId { get; set; }
        public string? Currency { get; set; } = "VND";
        public bool GroupByService { get; set; } = false;
    }

    public class ServiceRevenueDto
    {
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
        public decimal Percentage { get; set; } // Percentage of total revenue
    }

    public class CategoryServiceCountDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ServiceCount { get; set; }
        public string Description { get; set; }
    }
}