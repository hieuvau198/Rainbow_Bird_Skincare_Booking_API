using System;

namespace Application.DTOs
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; } = null!;
    }

    public class CreateBlogDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
    }

    public class UpdateBlogDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
