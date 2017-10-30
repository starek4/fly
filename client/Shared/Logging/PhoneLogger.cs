using System;

namespace Shared.Logging
{
    public class PhoneLogger : BaseLogger, ILogger
    {
        public void Error(string msg)
        {
            Write(LogEntryType.Error, msg);
        }

        public void Info(string msg)
        {
            Write(LogEntryType.Info, msg);
        }

        public void Fatal(string msg)
        {
            Write(LogEntryType.Fatal, msg);
        }

        public void Debug(string msg)
        {
            Write(LogEntryType.Debug, msg);
        }

        private void Write(LogEntryType type, string msg)
        {
            
        }
    }
}
