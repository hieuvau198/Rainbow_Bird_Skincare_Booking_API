using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class NewsHashtagDto
    {
        public int NewsId { get; set; }
        public int HashtagId { get; set; }
        public string Name { get; set; }
    }

    public class CreateNewsHashtagDto
    {
        [Required]
        public int NewsId { get; set; }

        [Required]
        public int HashtagId { get; set; }
    }
}
