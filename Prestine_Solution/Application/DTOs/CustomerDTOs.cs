

namespace Application.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string FullName { get; set; } = null!;
        public string? Preferences { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime? LastVisitAt { get; set; }
        public UserDto User { get; set; } = null!;
    }

    public class CreateCustomerDto
    {
        public int UserId { get; set; }
        public string? Preferences { get; set; }
        public string? MedicalHistory { get; set; }
    }
    public class CreateCustomerUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string FullName { get; set; } = null!;
        public string? Preferences { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime? LastVisitAt { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string? Preferences { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime? LastVisitAt { get; set; }
    }
}
