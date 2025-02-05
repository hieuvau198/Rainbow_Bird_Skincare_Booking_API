using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ServiceImage { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int DurationMinutes { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateServiceDto
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public IFormFile ServiceImage { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int DurationMinutes { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateServiceDto
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public IFormFile ServiceImage { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int DurationMinutes { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
