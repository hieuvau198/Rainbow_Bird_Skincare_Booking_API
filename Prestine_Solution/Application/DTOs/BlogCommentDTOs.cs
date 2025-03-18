using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class BlogCommentDto
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Content { get; set; }
        public int? Upvotes { get; set; }
        public int? Downvotes { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ShortBlogCommentDto> Replies { get; set; } = new List<ShortBlogCommentDto>();
    }

    public class ShortBlogCommentDto
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Content { get; set; }
        public int? Upvotes { get; set; }
        public int? Downvotes { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateBlogCommentDto
    {
        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Content { get; set; }
    }

    public class UpdateBlogCommentDto
    {
        public string Content { get; set; }
    }
}