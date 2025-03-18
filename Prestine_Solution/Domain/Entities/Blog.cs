using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual ICollection<BlogHashtag> BlogHashtags { get; set; } = new List<BlogHashtag>();

    public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
}
