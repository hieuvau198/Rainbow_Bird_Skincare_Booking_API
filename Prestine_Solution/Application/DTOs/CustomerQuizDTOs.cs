using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerQuizDto
    {
        public int CustomerQuizId { get; set; }
        public int CustomerId { get; set; }
        public int QuizId { get; set; }
        public int? TotalScore { get; set; }
        public string Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public CustomerDto Customer { get; set; } = null!;
        public QuizDto Quiz { get; set; } = null!;
    }

    public class CustomerQuizHistoryDto
    {
        public int CustomerQuizId { get; set; }
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerPoint { get; set; }
        public int TotalPoint { get; set; }
        public DateTime? TakingQuizDate { get; set; }
    }
    public class CreateCustomerQuizDto
    {
        public int CustomerId { get; set; }
        public int QuizId { get; set; }
    }

    public class UpdateCustomerQuizDto
    {
        public int? TotalScore { get; set; }
        public string Status { get; set; }
    }
}
