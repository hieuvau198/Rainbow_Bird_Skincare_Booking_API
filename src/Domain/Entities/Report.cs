using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Report : IEntity
    {
        public int Id { get; private set; }
        public int StaffId { get; private set; }
        public int ManagerId { get; private set; }
        public string ReportType { get; private set; }
        public string Content { get; private set; }
        public string Status { get; private set; }
        public DateTime ReportDate { get; private set; } = DateTime.UtcNow;

        public Staff Staff { get; private set; }
        public Manager Manager { get; private set; }

        public Report(int staffId, int managerId, string reportType, string content, string status)
        {
            StaffId = staffId;
            ManagerId = managerId;
            ReportType = reportType;
            Content = content;
            Status = status;
        }

        private Report(int id, int staffId, int managerId, string reportType, string content, string status, DateTime reportDate)
        {
            Id = id;
            StaffId = staffId;
            ManagerId = managerId;
            ReportType = reportType;
            Content = content;
            Status = status;
            ReportDate = reportDate;
        }

        // This constructor is used for EF Core to map database entities
        internal Report() { }
    }
}
