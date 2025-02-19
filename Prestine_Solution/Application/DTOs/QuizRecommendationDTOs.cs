using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class QuizRecommendationDto
    {
        public int RecommendationId { get; set; }
        public int QuizId { get; set; }
        public int ServiceId { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateQuizRecommendationDto
    {
        public int QuizId { get; set; }
        public int ServiceId { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
    }

    public class UpdateQuizRecommendationDto
    {
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public bool? IsActive { get; set; }
    }
}
