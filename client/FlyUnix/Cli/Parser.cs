using Logger.Enviroment;
using Logger.Logging;

namespace FlyUnix.Cli
{
    public static class Parser
    {
        private static readonly ILogger Logger;

        static Parser()
        {
            Logger = EnviromentHelper.GetLogger();
        }

        public static bool Parse(string[] args, ref Arguments arguments)
        {
            if (args.Length != 2)
            {
                Logger?.Fatal("Invalid arguments count.");
                return false;
            }

            if (args[0] == "-l")
            {
                arguments.Login = args[1];
                return true;
            }
            Logger?.Fatal("Failed to log in - credentials were not specified correctly.");
            return false;
        }
    }
}
