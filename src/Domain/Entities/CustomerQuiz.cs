using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CustomerQuiz
{
    public int CustomerQuizId { get; set; }

    public int CustomerId { get; set; }

    public int QuizId { get; set; }

    public int? TotalScore { get; set; }

    public string? Status { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomerAnswer> CustomerAnswers { get; set; } = new List<CustomerAnswer>();

    public virtual Quiz Quiz { get; set; } = null!;
}
