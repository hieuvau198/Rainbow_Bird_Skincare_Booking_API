using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Currency { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
