using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CustomerFeedback
{
    public int CustomerFeedbackId { get; set; }

    public int BookingId { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<CustomerFeedbackAnswer> CustomerFeedbackAnswers { get; set; } = new List<CustomerFeedbackAnswer>();
}
