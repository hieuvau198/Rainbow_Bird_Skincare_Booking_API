using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service : IEntity
    {
        public int Id { get; private set; }
        public string ServiceName { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public int DurationMinutes { get; private set; }
        public string Location { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Service(string serviceName, decimal price, string currency, int durationMinutes, string location, string description = null)
        {
            ServiceName = serviceName;
            Price = price;
            Currency = currency;
            DurationMinutes = durationMinutes;
            Location = location;
            Description = description;
        }
    }
}
