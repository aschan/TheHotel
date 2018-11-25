namespace BookingService
{
    using System.Collections.Generic;

    public class Booking
    {
        public IEnumerable<Guest> Guests { get; set; }

        public string RoomType { get; set; }

        public Hotel Hotel { get; set; }
    }
}
