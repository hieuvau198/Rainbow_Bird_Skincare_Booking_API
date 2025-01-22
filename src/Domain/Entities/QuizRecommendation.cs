using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class QuizRecommendation
{
    public int RecommendationId { get; set; }

    public int QuizId { get; set; }

    public int ServiceId { get; set; }

    public int? MinScore { get; set; }

    public int? MaxScore { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
