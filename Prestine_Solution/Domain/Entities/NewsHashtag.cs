using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class NewsHashtag
{
    public int NewsId { get; set; }

    public int HashtagId { get; set; }

    public virtual News News { get; set; } = null!;
}
