namespace OnlineBookings.Communication
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class AvailabilityException : Exception
    {
        public AvailabilityException()
            : base()
        {
        }
        
        public AvailabilityException(string message)
            : base(message)
        {
        }

        public AvailabilityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected AvailabilityException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
        
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info==null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }
    }
}
