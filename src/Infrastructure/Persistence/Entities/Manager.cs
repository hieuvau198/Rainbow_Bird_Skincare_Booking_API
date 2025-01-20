using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class Manager
{
    public int ManagerId { get; set; }

    public int UserId { get; set; }

    public string? Department { get; set; }

    public string? Responsibilities { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual User User { get; set; } = null!;
}
