using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TherapistDto
    {
        public int TherapistId { get; set; }
        public int UserId { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }

    public class CreateTherapistDto
    {
        public int UserId { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }

    public class UpdateTherapistDto
    {
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }
}