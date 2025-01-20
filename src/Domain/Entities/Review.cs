using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : IEntity
    {
        public int Id { get; private set; }
        public int BookingId { get; private set; }
        public int Rating { get; private set; }
        public string Feedback { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Booking Booking { get; private set; }

        public Review(int bookingId, int rating, string feedback)
        {
            BookingId = bookingId;
            Rating = rating;
            Feedback = feedback;
        }

        private Review(int id, int bookingId, int rating, string feedback, DateTime createdAt)
        {
            Id = id;
            BookingId = bookingId;
            Rating = rating;
            Feedback = feedback;
            CreatedAt = createdAt;
        }

        // This constructor is used for EF Core to map database entities
        internal Review() { }
    }
}
