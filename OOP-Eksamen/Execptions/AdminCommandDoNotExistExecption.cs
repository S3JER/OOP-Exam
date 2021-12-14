using System;
using System.Runtime.Serialization;

namespace OOP_Eksamen
{
    [Serializable]
    internal class AdminCommandDoNotExistExecption : Exception
    {
        public AdminCommandDoNotExistExecption()
        {
        }

        public AdminCommandDoNotExistExecption(string message) : base(message)
        {
        }

        public AdminCommandDoNotExistExecption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminCommandDoNotExistExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}