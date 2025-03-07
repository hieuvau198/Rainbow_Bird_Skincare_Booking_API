using System;

namespace Application.DTOs
{
    public class FeedbackAnswerDto
    {
        public int AnswerOptionId { get; set; }
        public int FeedbackQuestionId { get; set; }
        public string AnswerText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Reference to the question, included when needed
        public FeedbackQuestionBasicDto FeedbackQuestion { get; set; }
    }

    // A simplified question DTO to avoid circular references when including in answers
    public class FeedbackQuestionBasicDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }

    public class CreateFeedbackAnswerDto
    {
        public int FeedbackQuestionId { get; set; }
        public string AnswerText { get; set; }
    }

    public class UpdateFeedbackAnswerDto
    {
        public string AnswerText { get; set; }
    }
}