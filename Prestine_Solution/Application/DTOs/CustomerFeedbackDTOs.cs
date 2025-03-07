using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class CustomerFeedbackDto
    {
        public int CustomerFeedbackId { get; set; }
        public int BookingId { get; set; }
        public DateTime? SubmittedAt { get; set; }

        // Navigation properties
        public BookingBasicDto Booking { get; set; }
        public ICollection<CustomerFeedbackAnswerDto> CustomerFeedbackAnswers { get; set; } = new List<CustomerFeedbackAnswerDto>();
    }

    // A simplified booking DTO to avoid circular references
    public class BookingBasicDto
    {
        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        // Add other essential booking properties as needed
    }

    

    public class CreateCustomerFeedbackDto
    {
        public int BookingId { get; set; }
        public List<CreateCustomerFeedbackAnswerDto> Answers { get; set; } = new List<CreateCustomerFeedbackAnswerDto>();
    }

    

    public class UpdateCustomerFeedbackDto
    {
        // For feedback updates - typically limited properties since feedback submissions
        // are usually immutable after submission
        public DateTime? SubmittedAt { get; set; }
    }
}