using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CancelPolicy : IEntity
    {
        public int Id { get; private set; }
        public int MaxCancellations { get; private set; }
        public int WaitingTimeMinutes { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public CancelPolicy(int maxCancellations, int waitingTimeMinutes)
        {
            MaxCancellations = maxCancellations;
            WaitingTimeMinutes = waitingTimeMinutes;
        }
    }
}
