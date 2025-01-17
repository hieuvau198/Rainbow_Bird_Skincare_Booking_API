using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CancelBooking : IEntity
    {
        public int Id { get; private set; }
        public int BookingId { get; private set; }
        public DateTime CancelledAt { get; private set; } = DateTime.UtcNow;
        public string Reason { get; private set; }
        public bool IsRefunded { get; private set; }

        public Booking Booking { get; private set; }

        public CancelBooking(int bookingId, string reason, bool isRefunded)
        {
            BookingId = bookingId;
            Reason = reason;
            IsRefunded = isRefunded;
        }

        private CancelBooking(int id, int bookingId, DateTime cancelledAt, string reason, bool isRefunded)
        {
            Id = id;
            BookingId = bookingId;
            CancelledAt = cancelledAt;
            Reason = reason;
            IsRefunded = isRefunded;
        }

        // This constructor is used for EF Core to map database entities
        internal CancelBooking() { }
    }
}
