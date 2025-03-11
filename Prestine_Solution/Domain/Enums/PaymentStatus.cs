namespace Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,   // Payment has not been completed yet
        Paid = 1,      // Payment was successful
        Failed = 2,    // Payment attempt failed
        Refunded = 3,  // Payment was refunded
        Cancelled = 4  // Payment was cancelled before processing
    }
}
