using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking : IEntity
    {
        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public int TherapistId { get; private set; }
        public int ServiceId { get; private set; }
        public DateTime BookingDate { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Status { get; private set; }
        public string Notes { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Customer Customer { get; private set; }
        public Therapist Therapist { get; private set; }
        public Service Service { get; private set; }
            
        public Booking(int customerId, int therapistId, int serviceId, DateTime bookingDate, DateTime startTime, DateTime endTime, string status, string notes)
        {
            CustomerId = customerId;
            TherapistId = therapistId;
            ServiceId = serviceId;
            BookingDate = bookingDate;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            Notes = notes;
        }

        private Booking(int id, int customerId, int therapistId, int serviceId, DateTime bookingDate, DateTime startTime, DateTime endTime, string status, string notes, DateTime createdAt)
        {
            Id = id;
            CustomerId = customerId;
            TherapistId = therapistId;
            ServiceId = serviceId;
            BookingDate = bookingDate;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            Notes = notes;
            CreatedAt = createdAt;
        }

        // This constructor is used for EF Core to map database entities
        internal Booking() { }
    }
}
