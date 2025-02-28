using System;

namespace Domain.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }  // Optional blog image
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key: User who created the blog
        public int AuthorId { get; set; }
        public virtual User Author { get; set; } = null!;
    }
}
