using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string Content { get; set; } = null!;

    public int? Points { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CustomerAnswer> CustomerAnswers { get; set; } = new List<CustomerAnswer>();

    public virtual Question Question { get; set; } = null!;
}
