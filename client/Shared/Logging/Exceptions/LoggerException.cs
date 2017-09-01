using System;
using System.Runtime.Serialization;

namespace Shared.Logging.Exceptions
{
    public class LoggerException : Exception
    {
        private readonly StreamingContext context;
        public LoggerException() : base() { }
        public LoggerException(string message) : base(message) { }
        public LoggerException(string message, System.Exception inner) : base(message, inner) { }
        protected LoggerException(SerializationInfo info, StreamingContext context)
        {
            this.context = context;
        }
    }
}
