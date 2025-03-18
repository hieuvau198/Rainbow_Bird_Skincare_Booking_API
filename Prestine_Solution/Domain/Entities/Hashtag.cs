using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Hashtag
{
    public int HashtagId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
