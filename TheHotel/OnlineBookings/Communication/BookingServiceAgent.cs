namespace OnlineBookings.Communication
{
    using System;
    using System.Linq;

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
            ValidateGuest(guest, booking.Hotel.CountryCode);

            if (IsThereAnAvailableBed(booking))
            {
                // TODO: Add guest
            }
        }

        private Booking RetriveBooking(Guid bookingId)
        {
            var booking = _bookingSystem.FetchBooking(bookingId);
            if (booking == null)
                throw new BookingNotFoundException(bookingId);

            return booking;
        }

        private void ValidateGuest(IGuest guest, Country country)
        {
            if (string.IsNullOrWhiteSpace(guest.FirstName))
                throw new ArgumentException("First name must be supplied.", nameof(guest));

            if (string.IsNullOrWhiteSpace(guest.LastName))
                throw new ArgumentException("Last name must be supplied.", nameof(guest));

            if (country == Country.DE && string.IsNullOrEmpty(guest.Title))
                throw new ArgumentException("Title must be supplied in Germany.", nameof(guest));
        }

        private int GetNumberOfBeds(string roomType)
        {
            switch (roomType.ToUpperInvariant())
            {
                case "SINGLE":
                    return 1;
                case "DOUBLE":
                case "TWIN":
                    return 2;
                case "TRIPLE":
                    return 3;
                default:
                    return 0;
            }
        }

        private bool IsThereAnAvailableBed(Booking booking)
        {
            var numberOfBeds = GetNumberOfBeds(booking.RoomType);
            var numberOfGuests = booking.Guests.Count();
            return numberOfBeds - numberOfGuests > 0;
        }
    }
}
