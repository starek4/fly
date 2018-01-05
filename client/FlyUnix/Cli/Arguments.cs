using System;
using System.Security;

namespace FlyUnix.Cli
{
    public class Arguments
    {
        public string Login { get; set; }
        public SecureString Password { get; set; }
    }
}
