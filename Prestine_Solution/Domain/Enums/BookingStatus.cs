using System;
using System.Collections.Generic;

namespace Domain.Enums
{
    public enum BookingStatus
    {
        Pending = 0,                // Customer created the booking
        AwaitingConfirmation = 1,   // Staff is reviewing/editing the booking
        Confirmed = 2,              // Booking is confirmed and assigned to a therapist
        CheckedIn = 3,              // Customer has arrived
        InProgress = 4,             // Service is ongoing
        Completed = 5,              // Service is done
        CheckedOut = 6,             // Payment completed and booking finalized

        CancelledByCustomer = 7,    // Customer cancelled before confirmation
        CancelledByStaff = 8,       // Staff cancelled due to issues
        NoShow = 9,                 // Customer did not check in on time
        Incomplete = 10             // Service was started but not completed
    }
}
