using System;
using System.Diagnostics;
using Logger.Enviroment;
using Logger.Logging;

namespace FlyUnix
{
    public static class ApplicationKiller
    {
        private static readonly ILogger Logger = EnviromentHelper.GetLogger();
        private static void KillApp(string message)
        {
            Logger.Fatal(message);
            Console.WriteLine(message);
            Process.GetCurrentProcess().Kill();
        }

        public static void NetworkError()
        {
            KillApp("Error with network connection.");
        }

        public static void DatabaseError()
        {
            KillApp("Error with database connection or query.");
        }
    }
}
