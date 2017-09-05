using System;

namespace Shared.Logging.Exceptions
{
    public class LoggerException : Exception
    {
        public LoggerException(string message) : base(message) { }
    }
}
