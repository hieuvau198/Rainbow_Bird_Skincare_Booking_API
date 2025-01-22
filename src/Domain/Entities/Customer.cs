using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string? Preferences { get; set; }

    public string? MedicalHistory { get; set; }

    public DateTime? LastVisitAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<CustomerQuiz> CustomerQuizzes { get; set; } = new List<CustomerQuiz>();

    public virtual User User { get; set; } = null!;
}
