using System;
using System.Globalization;
using Plugin.SimpleLogger;

namespace Logger.Logging
{
    public class PhoneLogger : BaseLogger, ILogger
    {
        public PhoneLogger()
        {
            CrossSimpleLogger.Current.Configure("fly", 10, 500);
        }

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
            msg = FormatLog(DateTime.Now.ToString(CultureInfo.InvariantCulture), type, msg, Environment.NewLine);
            CrossSimpleLogger.Current.Info(msg);

        }
    }
}
