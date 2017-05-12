using System;
using System.Runtime.Serialization;

namespace Ramda.NET
{
    [Serializable]
    public class NaNException : ArithmeticException
    {
        public NaNException() : base(Exceptions.NotANumberException) {
        }

        public NaNException(string message) : base(message) {
        }

        public NaNException(string message, Exception innerException) : base(message, innerException) {
        }

        protected NaNException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
