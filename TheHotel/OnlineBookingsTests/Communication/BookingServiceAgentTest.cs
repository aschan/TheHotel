namespace OnlineBookings.Communication
{
    using System;

    using Moq;

    using NUnit.Framework;

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
    }
}
