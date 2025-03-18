using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BlogHashtag
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public int HashtagId { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual Hashtag Hashtag { get; set; } = null!;
}
