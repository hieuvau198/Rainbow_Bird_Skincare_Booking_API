using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public int UserId { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
        public UserDto User { get; set; } = null!;
    }

    public class CreateStaffDto
    {
        public int UserId { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
    }

    public class CreateStaffUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string FullName { get; set; } = null!;
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
    }


    public class UpdateStaffDto
    {
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
