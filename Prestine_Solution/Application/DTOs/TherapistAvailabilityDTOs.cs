using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TherapistAvailabilityDto
    {
        public int AvailabilityId { get; set; }
        public int TherapistId { get; set; }
        public int SlotId { get; set; }
        public DateOnly WorkingDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string TherapistName { get; set; } = string.Empty; // New field

        public TherapistDto Therapist { get; set; } = null!;
    }

    public class CreateTherapistAvailabilityDto
    {
        public int TherapistId { get; set; }
        public int SlotId { get; set; }
        public DateOnly WorkingDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class UpdateTherapistAvailabilityDto
    {
        public int? SlotId { get; set; }
        public DateOnly? WorkingDate { get; set; }
        public string? Status { get; set; }
    }

}
