using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ManagerDto
    {
        public int ManagerId { get; set; }
        public int UserId { get; set; }
        public string? Department { get; set; }
        public string? Responsibilities { get; set; }
        public DateTime? HireDate { get; set; }
    }
    public class CreateManagerDto
    {
        public int UserId { get; set; }
        public string? Department { get; set; }
        public string? Responsibilities { get; set; }
        public DateTime? HireDate { get; set; }
    }
    public class UpdateManagerDto
    {
        public string? Department { get; set; }
        public string? Responsibilities { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
