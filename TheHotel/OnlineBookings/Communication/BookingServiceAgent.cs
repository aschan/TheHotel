namespace OnlineBookings.Communication
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

        public void AddGuestToBooking(Guid bookingId, IGuest guest)
        {
            if (bookingId == Guid.Empty)
                throw new ArgumentNullException(nameof(bookingId));

            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            // TODO: Fetch booking
            // TODO: Validate guest using country from booking
            // TODO: Check available beds
            // TODO: Add guest

            throw new NotImplementedException();
        }
    }
}
