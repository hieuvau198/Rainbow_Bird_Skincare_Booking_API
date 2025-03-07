using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CustomerFeedbackAnswer
{
    public int ResponseId { get; set; }

    public int CustomerFeedbackId { get; set; }

    public string? AnswerText { get; set; }

    public int? SelectedAnswerOptionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CustomerFeedback CustomerFeedback { get; set; } = null!;
}
