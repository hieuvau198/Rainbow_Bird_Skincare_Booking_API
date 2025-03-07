using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class FeedbackAnswer
{
    public int AnswerOptionId { get; set; }

    public int FeedbackQuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual FeedbackQuestion FeedbackQuestion { get; set; } = null!;
}
