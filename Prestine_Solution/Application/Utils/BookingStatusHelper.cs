using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Application.Utils
{
    public static class BookingStatusHelper
    {
        public static List<BookingStatus> GetNextStatuses(BookingStatus currentStatus)
        {
            return currentStatus switch
            {
                BookingStatus.AwaitingConfirmation => new List<BookingStatus> { BookingStatus.Confirmed, BookingStatus.CancelledByCustomer, BookingStatus.CancelledByStaff },

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

        // ✅ Get User-Friendly Status Name
        public static string GetStatusDisplayName(BookingStatus status)
        {
            var type = typeof(BookingStatus);
            var memInfo = type.GetMember(status.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : status.ToString();
        }
    }
}
