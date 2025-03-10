using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Enums
{
    public enum BookingStatus
    {
        [Description("Awaiting Confirmation")] AwaitingConfirmation = 0,   // Staff is reviewing/editing the booking
        [Description("Confirmed")] Confirmed = 1,              // Booking is confirmed and assigned to a therapist
        [Description("Checked In")] CheckedIn = 2,            // Customer has arrived
        [Description("In Progress")] InProgress = 3,          // Service is ongoing
        [Description("Completed")] Completed = 4,             // Service is done
        [Description("Checked Out")] CheckedOut = 5,          // Payment completed and booking finalized

        [Description("Cancelled By Customer")] CancelledByCustomer = 6,    // Customer cancelled before confirmation
        [Description("Cancelled By Staff")] CancelledByStaff = 7,       // Staff cancelled due to issues
        [Description("No Show")] NoShow = 8,                 // Customer did not check in on time
        [Description("Incomplete")] Incomplete = 9             // Service was started but not completed
    }
}
