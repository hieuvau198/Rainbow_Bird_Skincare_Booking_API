using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CancelBookingDto
    {
        public int CancellationId { get; set; }
        public int BookingId { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? Reason { get; set; }
        public bool? IsRefunded { get; set; }
    }

    public class CreateCancelBookingDto
    {
        public int BookingId { get; set; }
        public string? Reason { get; set; }
        public bool? IsRefunded { get; set; }
    }

    public class UpdateCancelBookingDto
    {
        public string? Reason { get; set; }
        public bool? IsRefunded { get; set; }
    }
}
