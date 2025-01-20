using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AnswerRecommendation : IEntity
    {
        public int Id { get; private set; }
        public int AnswerId { get; private set; }
        public int ServiceId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Answer Answer { get; private set; }
        public Service Service { get; private set; }

        public AnswerRecommendation(int answerId, int serviceId)
        {
            AnswerId = answerId;
            ServiceId = serviceId;
        }

        private AnswerRecommendation(int id, int answerId, int serviceId, bool isActive, DateTime createdAt)
        {
            Id = id;
            AnswerId = answerId;
            ServiceId = serviceId;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        // This constructor is used for EF Core to map database entities
        internal AnswerRecommendation() { }
    }
}
