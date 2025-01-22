using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CustomerAnswer
{
    public int CustomerAnswerId { get; set; }

    public int CustomerQuizId { get; set; }

    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public int? PointsEarned { get; set; }

    public DateTime? AnsweredAt { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual CustomerQuiz CustomerQuiz { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
