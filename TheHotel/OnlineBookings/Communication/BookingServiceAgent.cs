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

            var booking = RetriveBooking(bookingId); 

            // TODO: Validate guest using country from booking
            // TODO: Check available beds
            // TODO: Add guest

            throw new NotImplementedException();
        }

        private Booking RetriveBooking(Guid bookingId)
        {
            var booking = _bookingSystem.FetchBooking(bookingId);
            if (booking == null)
                throw new BookingNotFoundException(bookingId);

            return booking;
        }
    }
}
