using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class EmailNotLegalException : Exception
    {
        public string email;
        public EmailNotLegalException()
        {
        }
        public EmailNotLegalException(string email)
        {
            this.email = email;
        }

        public EmailNotLegalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailNotLegalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}