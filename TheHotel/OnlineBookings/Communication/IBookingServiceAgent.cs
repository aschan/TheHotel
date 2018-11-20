namespace OnlineBookings.Communication
{
    using System;

    interface IBookingServiceAgent
    {
        void AddGuestToBooking(Guid bookingid, IGuest guest);
    }
}
