using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class CancelBooking
{
    public int CancellationId { get; set; }

    public int BookingId { get; set; }

    public DateTime? CancelledAt { get; set; }

    public string? Reason { get; set; }

    public bool? IsRefunded { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
