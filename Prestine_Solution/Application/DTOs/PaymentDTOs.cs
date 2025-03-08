using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal Tax { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
    }

    public class CreatePaymentDto
    {
        public decimal TotalAmount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public decimal Tax { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
    }

    public class UpdatePaymentDto
    {
        public string? Status { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
