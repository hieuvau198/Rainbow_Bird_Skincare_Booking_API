using System;

namespace Application.DTOs
{
    public class CustomerRatingDto
    {
        public int RatingId { get; set; }
        public int BookingId { get; set; }
        public int RatingValue { get; set; }
        public string ExperienceImageUrl { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Include basic booking info if needed
        public BookingDto Booking { get; set; }
    }

    public class CreateCustomerRatingDto
    {
        public int BookingId { get; set; }
        public int RatingValue { get; set; }
        public string ExperienceImageUrl { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateCustomerRatingDto
    {
        public int? RatingValue { get; set; }
        public string ExperienceImageUrl { get; set; }
        public string Comment { get; set; }
    }
}