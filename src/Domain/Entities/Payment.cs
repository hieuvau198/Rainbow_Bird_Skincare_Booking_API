using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : IEntity
    {
        public int Id { get; private set; }
        public int BookingId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public string PaymentMethod { get; private set; }
        public string Status { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string TransactionId { get; private set; }

        public Booking Booking { get; private set; }

        public Payment(int bookingId, decimal amount, string currency, string paymentMethod, string status, DateTime paymentDate, string transactionId)
        {
            BookingId = bookingId;
            Amount = amount;
            Currency = currency;
            PaymentMethod = paymentMethod;
            Status = status;
            PaymentDate = paymentDate;
            TransactionId = transactionId;
        }

        private Payment(int id, int bookingId, decimal amount, string currency, string paymentMethod, string status, DateTime paymentDate, string transactionId)
        {
            Id = id;
            BookingId = bookingId;
            Amount = amount;
            Currency = currency;
            PaymentMethod = paymentMethod;
            Status = status;
            PaymentDate = paymentDate;
            TransactionId = transactionId;
        }

        // This constructor is used for EF Core to map database entities
        internal Payment() { }
    }
}
