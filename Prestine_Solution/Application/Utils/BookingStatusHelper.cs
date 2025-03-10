using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Application.Utils
{
    public static class BookingStatusHelper
    {
        public static List<string> GetNextStatuses(string currentStatus)
        {
            BookingStatus? statusEnum = ParseBookingStatus(currentStatus);
            if (statusEnum == null)
                return new List<string>();

            return GetNextStatuses(statusEnum.Value)
                .Select(GetStatusDisplayName)
                .ToList();
        }

        public static List<BookingStatus> GetNextStatuses(BookingStatus currentStatus)
        {
            return currentStatus switch
            {
                BookingStatus.AwaitingConfirmation => new List<BookingStatus> { BookingStatus.Confirmed, BookingStatus.CancelledByCustomer, BookingStatus.CancelledByStaff },
                BookingStatus.Confirmed => new List<BookingStatus> { BookingStatus.CheckedIn, BookingStatus.CancelledByStaff, BookingStatus.NoShow },
                BookingStatus.CheckedIn => new List<BookingStatus> { BookingStatus.InProgress, BookingStatus.NoShow },
                BookingStatus.InProgress => new List<BookingStatus> { BookingStatus.Completed, BookingStatus.Incomplete },
                BookingStatus.Completed => new List<BookingStatus> { BookingStatus.CheckedOut },
                _ => new List<BookingStatus>()
            };
        }

        // ✅ Always return the user-friendly name for storing and displaying
        public static string GetStatusDisplayName(BookingStatus status)
        {
            var type = typeof(BookingStatus);
            var memInfo = type.GetMember(status.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : status.ToString();
        }

        // ✅ Store & process only human-readable names
        public static BookingStatus? ParseBookingStatus(string statusString)
        {
            foreach (BookingStatus status in Enum.GetValues(typeof(BookingStatus)))
            {
                string displayName = GetStatusDisplayName(status);
                if (string.Equals(displayName, statusString, StringComparison.OrdinalIgnoreCase))
                    return status;
            }

            return null; // ❌ If no match, return null
        }
    }
}
