using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BlogComment
{
    public int CommentId { get; set; }

    public int BlogId { get; set; }

    public int? UserId { get; set; }

    public int? ParentCommentId { get; set; }

    public string FullName { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string Content { get; set; } = null!;

    public int? Upvotes { get; set; }

    public int? Downvotes { get; set; }

    public bool? IsEdited { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual ICollection<BlogComment> InverseParentComment { get; set; } = new List<BlogComment>();

    public virtual BlogComment? ParentComment { get; set; }
}
