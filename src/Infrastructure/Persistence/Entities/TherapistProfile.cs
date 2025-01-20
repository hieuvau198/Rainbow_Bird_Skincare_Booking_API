using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class TherapistProfile
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

    public virtual Therapist Therapist { get; set; } = null!;
}
