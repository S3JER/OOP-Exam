using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class TooManyArgumentsException : Exception
    {
        public int length;

        public TooManyArgumentsException()
        {
        }

        public TooManyArgumentsException(int length)
        {
            this.length = length;
        }

        public TooManyArgumentsException(string message) : base(message)
        {
        }

        public TooManyArgumentsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManyArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}