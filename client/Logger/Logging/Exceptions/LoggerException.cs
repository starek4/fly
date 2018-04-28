using System;

namespace Logger.Logging.Exceptions
{
    public class LoggerException : Exception
    {
        public LoggerException(string message) : base(message) { }
    }
}
