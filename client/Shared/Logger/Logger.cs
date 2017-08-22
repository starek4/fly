using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Shared.Logger
{
    public static class Logger
    {
        private static readonly string path;

        static Logger()
        {
            // TODO: Find out where will we place the file on Mac
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = "/var/log/fly.log";
            }
            else
            {
                path = "fly.log";
            }
        }

        public static void Error(string msg)
        {
            File.AppendAllText(path, "ERROR: " + msg + Environment.NewLine);
        }

        public static void Info(string msg)
        {
            File.AppendAllText(path, "INFO: " + msg + Environment.NewLine);
        }

        public static void Fatal(string msg)
        {
            File.AppendAllText(path, "ERROR: " + msg + Environment.NewLine);
        }
    }
}
