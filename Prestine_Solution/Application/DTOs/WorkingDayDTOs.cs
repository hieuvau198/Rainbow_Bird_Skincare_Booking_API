using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WorkingDayDto
    {
        public int WorkingDayId { get; set; }
        public string DayName { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<int> TimeSlotIds { get; set; } = new List<int>();
    }

    public class CreateWorkingDayDto
    {
        public string DayName { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateWorkingDayDto
    {
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int? SlotDurationMinutes { get; set; }
        public bool? IsActive { get; set; }
    }
}
