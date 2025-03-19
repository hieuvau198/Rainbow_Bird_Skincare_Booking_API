using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? PaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string TransactionType { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public string? ReferenceNumber { get; set; }

    public string Sender { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public string? Description { get; set; }

    public string? SourceSystem { get; set; }

    public int? ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public decimal? TaxPercentage { get; set; }

    public decimal? TaxAmount { get; set; }
}
