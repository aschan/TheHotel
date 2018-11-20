namespace OnlineBookings.Communication
{
    using System;

    interface IBookingServiceAgent
    {
        void AddGuestToBooking(Guid bookingId, IGuest guest);
    }
}
