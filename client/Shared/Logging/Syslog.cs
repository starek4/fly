using System;
using System.Collections.Generic;
using System.IO;

namespace Shared.Logging
{
    public class Syslog
    {
        // TODO: Implement syslog UDP client...
        public void Send(LogEntryType type, Dictionary<LogEntryType, string> typeMapper, string msg)
        {
            // TEMP
            File.AppendAllText("fly.log", DateTime.Now + typeMapper[type] + msg + Environment.NewLine);
        }
    }
}
