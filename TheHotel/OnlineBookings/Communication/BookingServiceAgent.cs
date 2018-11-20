namespace OnlineBookings.CommunicationServices
{
    using System;

    using BookingService;

    public class BookingServiceAgent : IBookingServiceAgent
    {
        private readonly IBookingSystem _bookingSystem;

        public BookingServiceAgent(IBookingSystem bookingSystem)
        {
            _bookingSystem = bookingSystem ?? throw new ArgumentNullException(nameof(bookingSystem));
        }

        public void AddGuestToBooking(Guid bookingid, IGuest guest)
        {
            throw new NotImplementedException();
        }
    }
}
