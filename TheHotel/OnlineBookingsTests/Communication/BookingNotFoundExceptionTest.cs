namespace OnlineBookings.Communication
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using NUnit.Framework;

    [TestFixture]
    public class BookingNotFoundExceptionTest
    {
        [Test]
        public void AssertDefaultValueForBookingIdIsGuidEmpty()
        {
            var exceptions = new[]
                             {
                                 new BookingNotFoundException(),
                                 new BookingNotFoundException("message"),
                                 new BookingNotFoundException("message", new Exception())
                             };
            foreach (var exception in exceptions)
            {
                Assert.AreEqual(Guid.Empty, exception.BookingId);
            }
        }

        [Test]
        public void AssertValueForBookingIdIsSet()
        {
            var guid = new Guid("{26934B42-AD85-4E6E-B428-79A408702B68}");
            var exceptions = new[]
                             {
                                 new BookingNotFoundException(guid),
                                 new BookingNotFoundException(guid, "message"),
                                 new BookingNotFoundException(guid, "message", new Exception())
                             };
            foreach (var exception in exceptions)
            {
                Assert.AreEqual(guid, exception.BookingId);
            }
        }

        [Test]
        public void VerifyThatExceptionIsSerializable()
        {
            var guid = new Guid("{26934B42-AD85-4E6E-B428-79A408702B68}");
            var message = "This is The Message.";
            var innerException = new Exception("The Inner Exception");
            var initialException = new BookingNotFoundException(guid, message, innerException);

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, initialException);
            stream.Position = 0;
            var finalException = (BookingNotFoundException)formatter.Deserialize(stream);

            Assert.IsNotNull(finalException);
            Assert.AreEqual(initialException.BookingId, finalException.BookingId);
            Assert.AreEqual(initialException.Message, finalException.Message);
            Assert.IsNotNull(finalException.InnerException);
            Assert.AreEqual(initialException.InnerException.Message, finalException.InnerException.Message);
            Assert.AreEqual(initialException.InnerException.InnerException, finalException.InnerException.InnerException);
        }
    }
}
