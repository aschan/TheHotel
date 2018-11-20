namespace OnlineBookings.Communication
{
    using System;

    using BookingService;

    using Moq;

    using NUnit.Framework;

    using Guest = OnlineBookings.Guest;

    [TestFixture]
    public class BookingServiceAgentTest
    {
        [Test]
        [SetUICulture("en-US")]
        public void ValidateConstructorInjectionOfIBookingSystemThrowsAnException()
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
        public void ValidateHappyFlow()
        {
            var bookingSystemMock = new Mock<IBookingSystem>();
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
    }
}
