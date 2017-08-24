using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Shared.Logging
{
    public static class Logger
    {
        private static readonly string windowsPath = "fly.log";
        private static readonly Dictionary<LogEntryType, string> TypeMapper = new Dictionary<LogEntryType, string>
        {
            { LogEntryType.Fatal, "FATAL: "},
            { LogEntryType.Info, "INFO: "},
            { LogEntryType.Error, "ERROR: "}
        };

        public static void Error(string msg)
        {
            Write(LogEntryType.Error, msg);
        }

        public static void Info(string msg)
        {
            Write(LogEntryType.Info, msg);
        }

        public static void Fatal(string msg)
        {
            Write(LogEntryType.Fatal, msg);
        }

        private static void Write(LogEntryType type, string msg)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                File.AppendAllText(windowsPath, DateTime.Now + TypeMapper[type] + msg + Environment.NewLine);
            else if (RuntimeInformation.FrameworkDescription.Contains("Mono"))
            {
                // TODO: Phone logging...
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                new Syslog().Send(type,TypeMapper, msg);
            }
        }
    }
}
