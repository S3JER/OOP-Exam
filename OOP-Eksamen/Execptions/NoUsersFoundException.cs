using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class NoUsersFoundException : Exception
    {
        public int id;

        public NoUsersFoundException()
        {
        }

        public NoUsersFoundException(int id)
        {
            this.id = id;
        }

        public NoUsersFoundException(string message) : base(message)
        {
        }

        public NoUsersFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoUsersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}