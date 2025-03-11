using Domain.Enums;
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

        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNote { get; set; }
        public string? Location { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal BookingFee { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentStatus { get; set; }

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

        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNote { get; set; }
        public string? Location { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal BookingFee { get; set; }
    }


    public class UpdateBookingDto
    {
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int? PaymentId { get; set; }

        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNote { get; set; }
        public string? Location { get; set; }
        public decimal? ServicePrice { get; set; }
        public decimal? BookingFee { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentStatus { get; set; }
    }

    public class UpdateBookingTherapistDto
    {
        public int TherapistId { get; set; }
    }

    public class GetBookingStatusDto
    {
        public string CurrentStatus { get; set; } = string.Empty;
        public List<string> NextStatuses { get; set; } = new();
    }
}
