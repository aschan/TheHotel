namespace OnlineBookings.CommunicationServices
{
    using System;

    interface IBookingServiceAgent
    {
        void AddGuestToBooking(Guid bookingid, IGuest guest);
    }
}
