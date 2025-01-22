using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public string Content { get; set; } = null!;

    public int? Points { get; set; }

    public bool? IsMultipleChoice { get; set; }

    public int? DisplayOrder { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<CustomerAnswer> CustomerAnswers { get; set; } = new List<CustomerAnswer>();

    public virtual Quiz Quiz { get; set; } = null!;
}
