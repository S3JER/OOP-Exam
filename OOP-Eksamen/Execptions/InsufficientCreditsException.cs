using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    public class InsufficientCreditsException : Exception
    {
        public User user;
        public Product product;

        public InsufficientCreditsException()
        {
        }

        public InsufficientCreditsException(string message) : base(message)
        {
        }

        public InsufficientCreditsException(User user, Product product)
        {
            this.user = user;
            this.product = product;
        }

        public InsufficientCreditsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientCreditsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}