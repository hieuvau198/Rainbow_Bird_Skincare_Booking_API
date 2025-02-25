
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CancelPolicyDto
    {
        public int PolicyId { get; set; }
        public int? MaxCancellations { get; set; }
        public int? WaitingTimeMinutes { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateCancelPolicyDto
    {
        public int? MaxCancellations { get; set; }
        public int? WaitingTimeMinutes { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateCancelPolicyDto
    {
        public int? MaxCancellations { get; set; }
        public int? WaitingTimeMinutes { get; set; }
        public bool? IsActive { get; set; }
    }
}
