using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentPolicyDto
    {
        public int PolicyId { get; set; }
        public string? Currency { get; set; }
        public int? PaymentWindowHours { get; set; }
        public decimal? TaxPercentage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreatePaymentPolicyDto
    {
        public string? Currency { get; set; }
        public int? PaymentWindowHours { get; set; }
        public decimal? TaxPercentage { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdatePaymentPolicyDto
    {
        public string? Currency { get; set; }
        public int? PaymentWindowHours { get; set; }
        public decimal? TaxPercentage { get; set; }
        public bool? IsActive { get; set; }
    }
}
