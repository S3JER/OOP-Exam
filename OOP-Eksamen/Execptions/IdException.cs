using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class IdException : Exception
    {
        public int id;

        public IdException()
        {
        }

        public IdException(int id)
        {
            this.id = id;
        }

        public IdException(string message) : base(message)
        {
        }

        public IdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}