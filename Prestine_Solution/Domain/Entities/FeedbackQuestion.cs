using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FeedbackQuestion
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FeedbackAnswer> FeedbackAnswers { get; set; } = new List<FeedbackAnswer>();
}
