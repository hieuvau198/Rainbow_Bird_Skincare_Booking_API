using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CancelPolicy
{
    public int PolicyId { get; set; }

    public int? MaxCancellations { get; set; }

    public int? WaitingTimeMinutes { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
