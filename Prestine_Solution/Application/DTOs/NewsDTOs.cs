using System;

namespace Application.DTOs
{
    public class NewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public int PublisherId { get; set; }
        public string PublisherFullName { get; set; } = null!;
    }

    public class CreateNewsDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int PublisherId { get; set; }
    }

    public class UpdateNewsDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsPublished { get; set; }
    }
}
