using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CustomerRating
{
    public int RatingId { get; set; }

    public int BookingId { get; set; }

    public int RatingValue { get; set; }

    public string? ExperienceImageUrl { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
