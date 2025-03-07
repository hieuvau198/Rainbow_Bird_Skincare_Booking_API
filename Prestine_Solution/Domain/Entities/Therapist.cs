using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class Therapist
{
    public int TherapistId { get; set; }

    public int UserId { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Schedule { get; set; }

    public decimal? Rating { get; set; }

    public int? RatingCount { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<TherapistAvailability> TherapistAvailabilities { get; set; } = new List<TherapistAvailability>();

    public virtual TherapistProfile? TherapistProfile { get; set; }

    public virtual User User { get; set; } = null!;
}
