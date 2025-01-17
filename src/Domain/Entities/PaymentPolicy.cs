using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentPolicy : IEntity
    {
        public int Id { get; private set; }
        public decimal MinimumPaymentAmount { get; private set; }
        public string Currency { get; private set; }
        public int PaymentWindowHours { get; private set; }
        public bool RequiresDeposit { get; private set; }
        public decimal DepositPercentage { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public PaymentPolicy(decimal minimumPaymentAmount, string currency, int paymentWindowHours, bool requiresDeposit, decimal depositPercentage)
        {
            MinimumPaymentAmount = minimumPaymentAmount;
            Currency = currency;
            PaymentWindowHours = paymentWindowHours;
            RequiresDeposit = requiresDeposit;
            DepositPercentage = depositPercentage;
        }
    }
}
