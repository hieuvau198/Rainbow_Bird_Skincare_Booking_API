using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int? Points { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateAnswerDto
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int? Points { get; set; }
    }

    public class UpdateAnswerDto
    {
        public string Content { get; set; }
        public int? Points { get; set; }
        public bool? IsActive { get; set; }
    }
}
