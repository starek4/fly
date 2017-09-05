﻿using System;
using Shared.Logging.Exceptions;

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
            try
            {
                // TODO: Implement logging...
                throw new NotImplementedException(type + msg);
            }
            catch (Exception exception)
            {
                throw new LoggerException(exception.Message);
            }
        }
    }
}
