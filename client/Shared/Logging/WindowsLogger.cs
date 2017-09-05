using System;
using System.Globalization;
using System.IO;
using System.Security;
using Shared.Logging.Exceptions;

namespace Shared.Logging
{
    public class WindowsLogger : BaseLogger, ILogger
    {
        private const string Path = "fly.log";

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
                File.AppendAllText(Path, FormatLog(DateTime.Now.ToString(CultureInfo.InvariantCulture), type, msg, Environment.NewLine));
            }
            catch (Exception exception)
            {
                if (exception is PathTooLongException
                    || exception is DirectoryNotFoundException
                    || exception is IOException
                    || exception is UnauthorizedAccessException
                    || exception is NotSupportedException
                    || exception is SecurityException)
                {
                    throw new LoggerException(exception.Message);
                }
            }
        }
    }
}
