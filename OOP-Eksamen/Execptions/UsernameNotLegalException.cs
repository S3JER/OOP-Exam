using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class UsernameNotLegalException : Exception
    {
        public string username;
        public UsernameNotLegalException(string username)
        {
            this.username = username;
        }

        public UsernameNotLegalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernameNotLegalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}