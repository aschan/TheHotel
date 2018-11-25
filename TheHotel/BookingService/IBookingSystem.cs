namespace BookingService
{
    using System;

    public interface IBookingSystem
    {
        Booking FetchBooking(Guid bookingId);

        void AddGuestToBooking(Guid bookingId, Guest guest);
    }
}
