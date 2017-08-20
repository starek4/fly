using CommandLine;

namespace FlyUnix
{
    public class CommandLineOptions
    {
        [Option('l', "login", Required = true, HelpText = "Login to fly account.")]
        public string Login { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to fly account.")]
        public string Password { get; set; }
    }
}
