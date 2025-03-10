using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class BookingStatusHelper
    {
        public static List<BookingStatus> GetNextStatuses(BookingStatus currentStatus)
        {
            return currentStatus switch
            {
                BookingStatus.Pending => new List<BookingStatus> { BookingStatus.AwaitingConfirmation, BookingStatus.CancelledByCustomer },

                BookingStatus.AwaitingConfirmation => new List<BookingStatus> { BookingStatus.Confirmed, BookingStatus.CancelledByStaff },

                BookingStatus.Confirmed => new List<BookingStatus> { BookingStatus.CheckedIn, BookingStatus.CancelledByStaff, BookingStatus.NoShow },

                BookingStatus.CheckedIn => new List<BookingStatus> { BookingStatus.InProgress, BookingStatus.NoShow },

                BookingStatus.InProgress => new List<BookingStatus> { BookingStatus.Completed, BookingStatus.Incomplete },

                BookingStatus.Completed => new List<BookingStatus> { BookingStatus.CheckedOut },

                BookingStatus.CheckedOut => new List<BookingStatus>(), // Final status

                BookingStatus.CancelledByCustomer => new List<BookingStatus>(), // Final status

                BookingStatus.CancelledByStaff => new List<BookingStatus>(), // Final status

                BookingStatus.NoShow => new List<BookingStatus>(), // Final status

                BookingStatus.Incomplete => new List<BookingStatus>(), // Final status

                _ => new List<BookingStatus>() // Default case
            };
        }
    }
}
