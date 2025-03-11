using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int CustomerId { get; set; }

    public int? TherapistId { get; set; }

    public int ServiceId { get; set; }

    public int SlotId { get; set; }

    public int? PaymentId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal? ServicePrice { get; set; }

    public decimal BookingFee { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerNote { get; set; }

    public string? Location { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public bool IsRated { get; set; }

    public bool IsFeedback { get; set; }

    public string TherapistName { get; set; } = null!;

    public virtual CancelBooking? CancelBooking { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomerFeedback> CustomerFeedbacks { get; set; } = new List<CustomerFeedback>();

    public virtual ICollection<CustomerRating> CustomerRatings { get; set; } = new List<CustomerRating>();

    public virtual Payment? Payment { get; set; }

    public virtual Review? Review { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual TimeSlot Slot { get; set; } = null!;
}
