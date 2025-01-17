using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; } // Consider using a secure hashing mechanism
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; private set; }

        // ... other properties and methods

        public User(string username, string password, string email, string phone, string fullName, string role)
        {
            Username = username;
            Password = password; // Hash password before storing
            Email = email;
            Phone = phone;
            FullName = fullName;
            Role = role;
        }
    }
}
