﻿using System.Collections.Generic;

namespace Logger.Logging
{
    public class BaseLogger
    {
        private Dictionary<LogEntryType, string> TypeMapper { get; } = new Dictionary<LogEntryType, string>
        {
            {LogEntryType.Fatal, "FATAL: "},
            {LogEntryType.Info, "INFO: "},
            {LogEntryType.Error, "ERROR: "},
            {LogEntryType.Debug, "DEBUG: " }
        };

        public string FormatLog(string dateTime, LogEntryType type, string msg, string newline)
        {
            return dateTime + " " + TypeMapper[type] + msg + newline;
        }
    }
}
