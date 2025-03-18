using System;

namespace Application.DTOs
{
    public class HashtagDto
    {
        public int HashtagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateHashtagDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateHashtagDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}