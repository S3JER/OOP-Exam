using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    internal class ProductNotActiveException : Exception
    {
        public ProductNotActiveException()
        {
        }

        public ProductNotActiveException(string message) : base(message)
        {
        }

        public ProductNotActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}