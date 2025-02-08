using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TimeSlotDto
    {
        public int SlotId { get; set; }
        public int WorkingDayId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int SlotNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateTimeSlotDto
    {
        public int WorkingDayId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int SlotNumber { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateTimeSlotDto
    {
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
