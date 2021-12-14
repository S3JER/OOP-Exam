using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public string id;
        public ProductNotFoundException(string id)
        {
            this.id = id;
        }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}