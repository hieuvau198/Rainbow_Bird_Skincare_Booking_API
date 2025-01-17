using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Therapist : User
    {
        public string Specialization { get; private set; }
        public string Bio { get; private set; }
        public bool IsAvailable { get; private set; }
        public string Schedule { get; private set; } // Consider a dedicated Schedule class if needed
        public decimal? Rating { get; private set; }

        public Therapist(string username, string password, string email, string phone, string fullName, string role, string specialization, string bio, bool isAvailable, string schedule, decimal? rating)
            : base(username, password, email, phone, fullName, role)
        {
            Specialization = specialization;
            Bio = bio;
            IsAvailable = isAvailable;
            Schedule = schedule;
            Rating = rating;
        }
    }
}
