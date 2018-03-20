using System;
using System.Diagnostics;
using Logger.Enviroment;

namespace FlyUnix
{
    public static class ShellHandler
    {
        private static string ExecuteBashCommand(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        public static string GetDeviceId()
        {
            return GetDeviceName();
        }

        public static string GetDeviceName()
        {
            return ExecuteBashCommand("uname -n");
        }

        public static void Shutdown()
        {
            if (EnviromentHelper.GetPlatformType() == PlatformType.Linux)
                ExecuteBashCommand("shutdown -h");
            else if (EnviromentHelper.GetPlatformType() == PlatformType.Osx)
                ExecuteBashCommand("sudo shutdown -h +1");
            else
                throw new NotImplementedException("This system is not supported.");
        }
    }
}
