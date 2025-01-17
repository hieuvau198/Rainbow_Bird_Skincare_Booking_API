using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Manager : User
    {
        public string Responsibilities { get; private set; }

        public Manager(string username, string password, string email, string phone, string fullName, string role, string responsibilities)
            : base(username, password, email, phone, fullName, role)
        {
            Responsibilities = responsibilities;
        }
    }
}
