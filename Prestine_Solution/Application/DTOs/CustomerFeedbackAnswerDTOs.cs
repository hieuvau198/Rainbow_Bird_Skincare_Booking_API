using System;

namespace Application.DTOs
{
    public class CustomerFeedbackAnswerDto
    {
        public int ResponseId { get; set; }
        public int CustomerFeedbackId { get; set; }
        public string? AnswerText { get; set; }
        public int? SelectedAnswerOptionId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateCustomerFeedbackAnswerDto
    {
        public int CustomerFeedbackId { get; set; }
        public string? AnswerText { get; set; }
        public int? SelectedAnswerOptionId { get; set; }
    }

    public class UpdateCustomerFeedbackAnswerDto
    {
        public string? AnswerText { get; set; }
        public int? SelectedAnswerOptionId { get; set; }
    }
}