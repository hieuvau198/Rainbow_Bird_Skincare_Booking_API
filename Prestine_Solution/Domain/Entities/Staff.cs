using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public int UserId { get; set; }

    public string? Department { get; set; }

    public string? Position { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual User User { get; set; } = null!;
}
