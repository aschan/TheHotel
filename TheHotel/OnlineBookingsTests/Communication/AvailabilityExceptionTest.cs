namespace OnlineBookings.Communication
{
	using System;
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;

	using NUnit.Framework;

	[TestFixture]
	public class AvailabilityExceptionTest
	{
		[Test]
		public void VerifyDefaultConstructor()
		{
			var exception = new AvailabilityException();
			Assert.IsNotNull(exception);
			Assert.IsInstanceOf<AvailabilityException>(exception);
			Assert.IsNotNull(exception.Message);
			Assert.AreEqual("Exception of type 'OnlineBookings.Communication.AvailabilityException' was thrown.", exception.Message);
			Assert.IsNull(exception.StackTrace);
			Assert.IsNull(exception.InnerException);
		}

		[Test]
		public void VerifyThatExceptionIsSerializable()
		{
			var message = "This is The Message.";
			var innerException = new Exception("The Inner Exception");
			var initialException = new AvailabilityException(message, innerException);

			var formatter = new BinaryFormatter();
			var stream = new MemoryStream();
			formatter.Serialize(stream, initialException);
			stream.Position = 0;
			var finalException = (AvailabilityException)formatter.Deserialize(stream);

			Assert.IsNotNull(finalException);
			Assert.AreEqual(initialException.Message, finalException.Message);
			Assert.IsNotNull(finalException.InnerException);
			Assert.AreEqual(initialException.InnerException.Message, finalException.InnerException.Message);
			Assert.AreEqual(initialException.InnerException.InnerException, finalException.InnerException.InnerException);
		}
	}
}
