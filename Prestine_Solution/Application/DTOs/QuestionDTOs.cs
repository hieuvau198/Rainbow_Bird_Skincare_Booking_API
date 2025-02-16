using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public int? Points { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateQuestionDto
    {
        public int QuizId { get; set; }
        public string Content { get; set; }
        public int? Points { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int? DisplayOrder { get; set; }
    }

    public class UpdateQuestionDto
    {
        public string Content { get; set; }
        public int? Points { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
