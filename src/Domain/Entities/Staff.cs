using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Staff : User
    {
        public string Department { get; private set; }
        public string Position { get; private set; }
        public DateTime HireDate { get; private set; }

        public Staff(string username, string password, string email, string phone, string fullName, string role, string department, string position, DateTime hireDate)
            : base(username, password, email, phone, fullName, role)
        {
            Department = department;
            Position = position;
            HireDate = hireDate;
        }
    }
}
