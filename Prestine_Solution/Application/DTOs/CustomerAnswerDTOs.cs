using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerAnswerDto
    {
        public int CustomerAnswerId { get; set; }
        public int CustomerQuizId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int? PointsEarned { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public CustomerQuizDto CustomerQuiz { get; set; } = null!;
        public QuestionDto Question { get; set; } = null!;
        public AnswerDto Answer { get; set; } = null!;
    }

    public class CreateCustomerAnswerDto
    {
        public int CustomerQuizId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int? PointsEarned { get; set; }
    }

    public class UpdateCustomerAnswerDto
    {
        public int? PointsEarned { get; set; }
    }
}
