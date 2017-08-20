using System.IO;

namespace FlyUnix
{
    public static class Logger
    {
        private static StreamWriter LoggerWriter { get; set; } = new StreamWriter("fly.log", true);

        public static void Error(string msg)
        {
            LoggerWriter.WriteLine("ERROR: " + msg);
            LoggerWriter.Flush();
        }

        public static void Info(string msg)
        {
            LoggerWriter.WriteLine("INFO: " + msg);
            LoggerWriter.Flush();
        }

        public static void Fatal(string msg)
        {
            LoggerWriter.WriteLine("FATAL: " + msg);
            LoggerWriter.Flush();
        }
    }
}
