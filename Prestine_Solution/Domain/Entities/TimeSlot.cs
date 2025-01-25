using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TimeSlot
{
    public int SlotId { get; set; }

    public int WorkingDayId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<TherapistAvailability> TherapistAvailabilities { get; set; } = new List<TherapistAvailability>();

    public virtual WorkingDay WorkingDay { get; set; } = null!;
}
