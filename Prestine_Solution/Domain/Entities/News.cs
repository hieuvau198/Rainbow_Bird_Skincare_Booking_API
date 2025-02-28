using System;

namespace Domain.Entities
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }  // Optional news image
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;

        // Foreign Key: User who published the news
        public int PublisherId { get; set; }
        public virtual User Publisher { get; set; } = null!;
    }
}
