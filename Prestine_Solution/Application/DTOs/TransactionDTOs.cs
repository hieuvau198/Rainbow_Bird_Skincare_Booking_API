using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int? PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Description { get; set; }
        public string SourceSystem { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
    }

    public class TransactionFilterDto
    {
        // Only keep essential filters
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
