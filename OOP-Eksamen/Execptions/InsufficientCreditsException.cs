using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    internal class InsufficientCreditsException : Exception
    {
        private User user;
        private Product product;
        private string v;

        public InsufficientCreditsException()
        {
        }

        public InsufficientCreditsException(string message) : base(message)
        {
        }

        public InsufficientCreditsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InsufficientCreditsException(User user, Product product, string v)
        {
            this.user = user;
            this.product = product;
            this.v = v;
        }

        protected InsufficientCreditsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}