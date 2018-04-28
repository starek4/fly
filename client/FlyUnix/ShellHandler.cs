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

        public static void Restart()
        {
            if (EnviromentHelper.GetPlatformType() == PlatformType.Linux)
                ExecuteBashCommand("shutdown -r");
            else if (EnviromentHelper.GetPlatformType() == PlatformType.Osx)
                ExecuteBashCommand("sudo shutdown -r +1");
            else
                throw new NotImplementedException("This system is not supported.");
        }

        public static void Sleep()
        {
            if (EnviromentHelper.GetPlatformType() == PlatformType.Linux)
            {
                string result = ExecuteBashCommand("systemctl suspend");
                if (result != String.Empty)
                {
                    string notImplementedMessage = "Sleep is not implemented on this platform.";
                    EnviromentHelper.GetLogger().Info(notImplementedMessage);
                    Console.WriteLine(notImplementedMessage);
                }
            }
            else if (EnviromentHelper.GetPlatformType() == PlatformType.Osx)
                ExecuteBashCommand("pmset sleepnow");
            else
                throw new NotImplementedException("This system is not supported.");
        }

        public static void Mute()
        {
            if (EnviromentHelper.GetPlatformType() == PlatformType.Linux)
            {
                ExecuteBashCommand("amixer set Master toggle");
            }
            else if (EnviromentHelper.GetPlatformType() == PlatformType.Osx)
                ExecuteBashCommand("osascript -e \'set volume output muted true\'");
            else
                throw new NotImplementedException("This system is not supported.");
        }
    }
}
