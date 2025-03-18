using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Hashtag
{
    public int HashtagId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BlogHashtag> BlogHashtags { get; set; } = new List<BlogHashtag>();

    public virtual ICollection<NewsHashtag> NewsHashtags { get; set; } = new List<NewsHashtag>();
}
