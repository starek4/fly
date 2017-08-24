using System.Collections.Generic;
using Shared.Logging;

namespace Shared.CLI
{
    public static class Parser
    {
        public static bool Parse(string[] args, ref Arguments arguments)
        {
            if (args.Length != 4)
            {
                Logger.Fatal("Bad arguments count.");
                return false;
            }

            Dictionary<string, string> parsedArgs = new Dictionary<string, string>()
            {
                {args[0], args[1]},
                { args[2], args[3]}
            };

            if (parsedArgs != null && parsedArgs.ContainsKey("-l") && parsedArgs.ContainsKey("-p"))
            {
                arguments.Login = parsedArgs["-l"];
                arguments.Password = parsedArgs["-p"];
                return true;
            }
            Logger.Fatal("Failed to log in - credentials were not specified correctly.");
            return false;
        }
    }
}
