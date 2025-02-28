using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TherapistDto
    {
        public int TherapistId { get; set; }
        public int UserId { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
        public UserDto User { get; set; } = null!; 
    }
    public class CreateTherapistUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string FullName { get; set; } = null!;
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }

    public class CreateTherapistDto
    {
        public int UserId { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }

    public class UpdateTherapistDto
    {
        public bool? IsAvailable { get; set; }
        public string? Schedule { get; set; }
        public decimal? Rating { get; set; }
    }
}