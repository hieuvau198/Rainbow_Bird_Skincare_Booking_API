using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BlogHashtagDto
    {
        public int BlogId { get; set; }
        public int HashtagId { get; set; }
        public string Tittle { get; set; }
    }

    public class CreateBlogHashtagDto
    {
        [Required]
        public int BlogId { get; set; }

        [Required]
        public int HashtagId { get; set; }
    }
}
