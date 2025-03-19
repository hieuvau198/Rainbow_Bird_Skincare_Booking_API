using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; } = 1000000;
        public string? Currency { get; set; } = "VND";
        public string? PaymentMethod { get; set; } = "Cash";
        public string? Status { get; set; } = "Pending";
        public decimal Tax { get; set; }
        public string? Sender { get; set; } = "Prestine Care Customer";
        public string? Receiver { get; set; } = "Prestine Care";
    }

    public class UpdatePaymentDto
    {
        public string? Status { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
