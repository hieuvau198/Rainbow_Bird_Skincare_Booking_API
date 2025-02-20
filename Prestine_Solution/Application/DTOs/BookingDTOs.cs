using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int TherapistId { get; set; }
        public int ServiceId { get; set; }
        public int SlotId { get; set; }
        public int? PaymentId { get; set; }
        public DateOnly BookingDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateBookingDto
    {
        public int CustomerId { get; set; }
        public int TherapistId { get; set; }
        public int ServiceId { get; set; }
        public int SlotId { get; set; }
        public DateOnly BookingDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateBookingDto
    {
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int? PaymentId { get; set; }
    }
}
