using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PaymentPolicy
{
    public int PolicyId { get; set; }

    public string? Currency { get; set; }

    public int? PaymentWindowHours { get; set; }

    public decimal? TaxPercentage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
