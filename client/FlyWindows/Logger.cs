using System;
using System.IO;

namespace FlyWindows
{
    public static class Logger
    {
        private static StreamWriter LoggerWriter { get; set; } = new StreamWriter("fly.log", true);

        public static void Error(string msg)
        {
            LoggerWriter.WriteLine(DateTime.Now + "ERROR: " + msg);
            LoggerWriter.Flush();
        }

        public static void Info(string msg)
        {
            LoggerWriter.WriteLine(DateTime.Now + "INFO: " + msg);
            LoggerWriter.Flush();
        }

        public static void Fatal(string msg)
        {
            LoggerWriter.WriteLine(DateTime.Now + "FATAL: " + msg);
            LoggerWriter.Flush();
        }
    }
}
