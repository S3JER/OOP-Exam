using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class ProductIdNotIntException : Exception
    {
        public ProductIdNotIntException()
        {
        }

        public ProductIdNotIntException(string message) : base(message)
        {
        }

        public ProductIdNotIntException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductIdNotIntException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}