using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class TherapistAvailability
{
    public int AvailabilityId { get; set; }

    public int TherapistId { get; set; }

    public int SlotId { get; set; }

    public DateOnly WorkingDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TimeSlot Slot { get; set; } = null!;

    public virtual Therapist Therapist { get; set; } = null!;
}
