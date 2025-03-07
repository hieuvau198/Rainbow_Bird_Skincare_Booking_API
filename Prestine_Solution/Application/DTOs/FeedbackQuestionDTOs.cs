using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class FeedbackQuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<FeedbackAnswerDto> FeedbackAnswers { get; set; } = new List<FeedbackAnswerDto>();
    }

    public class CreateFeedbackQuestionDto
    {
        public string QuestionText { get; set; }
    }

    public class UpdateFeedbackQuestionDto
    {
        public string QuestionText { get; set; }
    }

    
}