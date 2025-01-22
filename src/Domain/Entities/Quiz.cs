using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Quiz
{
    public int QuizId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Category { get; set; }

    public int? TotalPoints { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CustomerQuiz> CustomerQuizzes { get; set; } = new List<CustomerQuiz>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizRecommendation> QuizRecommendations { get; set; } = new List<QuizRecommendation>();
}
