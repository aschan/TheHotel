namespace OnlineBookings.Communication
{
    using System;
    using System.Collections.Generic;

    using BookingService;

    using Moq;

    using NUnit.Framework;

    using Guest = OnlineBookings.Guest;

    [TestFixture]
    public class BookingServiceAgentTest
    {
        [Test]
        [SetUICulture("en-US")]
        public void VerifyThatConstructorInjectionOfIBookingSystemThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new BookingServiceAgent(null));
            Assert.That(exception.ParamName, Is.EqualTo("bookingSystem"));
        }

        [Test]
        public void VerifyInputValidationOfGuid()
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
            var bookingServiceAgent = new BookingServiceAgent(bookingSystemMock.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => bookingServiceAgent.AddGuestToBooking(Guid.Empty, Mock.Of<IGuest>()));
            Assert.That(exception.ParamName, Is.EqualTo("bookingId"));
        }

        [Test]
        public void VerifyInputValidationOfGuest()
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
            var bookingServiceAgent = new BookingServiceAgent(bookingSystemMock.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => bookingServiceAgent.AddGuestToBooking(Guid.NewGuid(), null));
            Assert.That(exception.ParamName, Is.EqualTo("guest"));
        }

        [Test]
        public void VerifyThatBookingNotFoundExceptionIsThrownWhenNoBookingMatches()
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
            bookingSystemMock.Setup(b => b.FetchBooking(It.IsAny<Guid>())).Returns<Booking>(null);
            var bookingServiceAgent = new BookingServiceAgent(bookingSystemMock.Object);

            var guid = Guid.NewGuid();
            IGuest guest = new Guest
                           {
                               FirstName = "Arnold",
                               LastName = "Stallone",
                               Title = "Action"
                           };

            var exception = Assert.Throws<BookingNotFoundException>(() => bookingServiceAgent.AddGuestToBooking(guid, guest));
            Assert.That(exception.BookingId, Is.EqualTo(guid));
            bookingSystemMock.Verify(b => b.FetchBooking(It.IsAny<Guid>()), Times.Once);
            bookingSystemMock.Verify(b => b.AddGuestToBooking(It.IsAny<Guid>(), It.IsAny<BookingService.Guest>()), Times.Never);
        }

        [Test]
        [TestCase("", "Stallone", null, Country.SE, "First")]
        [TestCase("Arnold", "", null, Country.DK, "Last")]
        [TestCase(null, "Stallone", "Action", Country.SE, "First")]
        [TestCase("Arnold", null, "Dejligt", Country.DK, "Last")]
        [TestCase("Arnold", "Stallone", "", Country.DE, "Title")]
        [TestCase("Arnold", "Stallone", null, Country.DE, "Title")]
        public void VerifyThatValidGuestsValidate(string firstName, string lastName, string title, Country country, string startOfMessage)
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
            bookingSystemMock.Setup(b => b.FetchBooking(It.IsAny<Guid>())).Returns(CreateBooking(firstName, lastName, title, country, "TWIN"));
            var bookingServiceAgent = new BookingServiceAgent(bookingSystemMock.Object);

            var guid = Guid.NewGuid();
            var guest = new Guest
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Title = title,
                        };
            
            var exception = Assert.Throws<ArgumentException>(() => bookingServiceAgent.AddGuestToBooking(guid, guest));

            Assert.That(exception.ParamName, Is.EqualTo("guest"));
            Assert.That(exception.Message.StartsWith(startOfMessage));
            bookingSystemMock.Verify(b => b.FetchBooking(It.IsAny<Guid>()), Times.Once);
            bookingSystemMock.Verify(b => b.AddGuestToBooking(It.IsAny<Guid>(), It.IsAny<BookingService.Guest>()), Times.Never);

        }

        [Test]
        [TestCase("Arnold", "Stallone", null, Country.SE)]
        [TestCase("Arnold", "Stallone", null, Country.DK)]
        [TestCase("Arnold", "Stallone", "Action", Country.SE)]
        [TestCase("Arnold", "Stallone", "Dejligt", Country.DK)]
        [TestCase("Arnold", "Stallone", "Herr", Country.DE)]
        public void ValidateHappyFlow(string firstName, string lastName, string title, Country country)
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
            bookingSystemMock.Setup(b => b.FetchBooking(It.IsAny<Guid>())).Returns(CreateBooking(firstName, lastName, title, country, "TWIN"));
            var bookingServiceAgent = new BookingServiceAgent(bookingSystemMock.Object);

            IGuest guest = new Guest
                           {
                               FirstName = "Arnold",
                               LastName = "Stallone",
                               Title = "Action"
                           };
            bookingServiceAgent.AddGuestToBooking(Guid.NewGuid(), guest);

            bookingSystemMock.Verify(b => b.FetchBooking(It.IsAny<Guid>()), Times.Once);
            bookingSystemMock.Verify(b => b.AddGuestToBooking(It.IsAny<Guid>(), It.IsAny<BookingService.Guest>()), Times.Once);
        }

        private BookingService.Booking CreateBooking(string firstName, string lastName, string title, Country country, string roomType)
        {
            var guest = new BookingService.Guest
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Title = title,
                        };
            var hotel = new BookingService.Hotel
                        {
                            CountryCode = country,
                            Name = "Hotel California",
                        };
            return new BookingService.Booking
                   {
                       Guests = new List<BookingService.Guest>() {guest},
                       Hotel = hotel,
                       RoomType = roomType
                   };
        }
    }
}
