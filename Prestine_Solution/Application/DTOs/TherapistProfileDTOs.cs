using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TherapistProfileDto
    {
        public int ProfileId { get; set; }
        public int TherapistId { get; set; }
        public string? Bio { get; set; }
        public string? PersonalStatement { get; set; }
        public string? ProfileImage { get; set; }
        public string? Education { get; set; }
        public string? Certifications { get; set; }
        public string? Specialties { get; set; }
        public string? Languages { get; set; }
        public int? YearsExperience { get; set; }
        public bool? AcceptsNewClients { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateTherapistProfileDto
    {
        public string? Bio { get; set; }
        public string? PersonalStatement { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? Education { get; set; }
        public string? Certifications { get; set; }
        public string? Specialties { get; set; }
        public string? Languages { get; set; }
        public int? YearsExperience { get; set; }
        public bool AcceptsNewClients { get; set; } = true;
    }

    public class UpdateTherapistProfileDto
    {
        public string? Bio { get; set; }
        public string? PersonalStatement { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? Education { get; set; }
        public string? Certifications { get; set; }
        public string? Specialties { get; set; }
        public string? Languages { get; set; }
        public int? YearsExperience { get; set; }
        public bool? AcceptsNewClients { get; set; }
    }
}
