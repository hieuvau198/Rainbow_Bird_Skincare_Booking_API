using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : User
    {
        public string Preferences { get; private set; }
        public string MedicalHistory { get; private set; }
        public DateTime? LastVisitAt { get; private set; }

        public Customer(string username, string password, string email, string phone, string fullName, string role, string preferences, string medicalHistory)
            : base(username, password, email, phone, fullName, role)
        {
            Preferences = preferences;
            MedicalHistory = medicalHistory;
        }
    }
}
