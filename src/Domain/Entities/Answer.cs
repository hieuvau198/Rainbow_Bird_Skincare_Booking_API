using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Question Question { get; private set; }

        public Answer(int questionId, string description)
        {
            QuestionId = questionId;
            Description = description;
        }

        private Answer(int id, int questionId, string description, bool isActive, DateTime createdAt)
        {
            Id = id;
            QuestionId = questionId;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        // This constructor is used for EF Core to map database entities
        internal Answer() { }
    }
}
