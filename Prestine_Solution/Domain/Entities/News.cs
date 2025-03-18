using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime PublishedAt { get; set; }

    public bool IsPublished { get; set; }

    public int PublisherId { get; set; }

    public virtual ICollection<NewsHashtag> NewsHashtags { get; set; } = new List<NewsHashtag>();

    public virtual User Publisher { get; set; } = null!;

    public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
}
