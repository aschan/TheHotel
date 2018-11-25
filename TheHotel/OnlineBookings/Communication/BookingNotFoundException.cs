namespace OnlineBookings.Communication
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException()
        {
        }

        public BookingNotFoundException(string message)
            : base(message)
        {
        }

        public BookingNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BookingNotFoundException(Guid bookingId)
        {
            BookingId = bookingId;
        }
        
        public BookingNotFoundException(Guid bookingId, string message)
            : base(message)
        {
            BookingId = bookingId;
        }

        public BookingNotFoundException(Guid bookingId, string message, Exception innerException)
            : base(message, innerException)
        {
            BookingId = bookingId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected BookingNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            BookingId = (Guid)info.GetValue(nameof(BookingId), typeof(Guid));
        }

        public Guid BookingId { get; set; }
        
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info==null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(BookingId), BookingId, typeof(Guid));
            base.GetObjectData(info, context);
        }
    }
}
