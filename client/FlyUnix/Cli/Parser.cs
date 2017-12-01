using System.Collections.Generic;
using Shared.Enviroment;
using Shared.Logging;

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
            if (args.Length != 4)
            {
                Logger?.Fatal("Invalid arguments count.");
                return false;
            }

            if (args[0] == args[2])
                return false;

            Dictionary<string, string> parsedArgs = new Dictionary<string, string>()
            {
                {args[0], args[1]},
                {args[2], args[3]}
            };

            if (parsedArgs.ContainsKey("-l") && parsedArgs.ContainsKey("-p"))
            {
                arguments.Login = parsedArgs["-l"];
                arguments.Password = parsedArgs["-p"];
                return true;
            }
            Logger?.Fatal("Failed to log in - credentials were not specified correctly.");
            return false;
        }
    }
}
