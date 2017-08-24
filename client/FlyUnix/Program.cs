using System;
using System.Threading;
using Shared.API;
using CommandLine;
using EventLogger;

namespace FlyUnix
{
    class Program
    {
        static void Main(string[] args)
        {
            string deviceId = ShellHandler.GetDeviceId();
            string deviceName = ShellHandler.GetDeviceName();

            deviceId = deviceId.Replace(Environment.NewLine, String.Empty);
            deviceName = deviceName.Replace(Environment.NewLine, String.Empty);

            Client client = new Client();
            CommandLineOptions options = new CommandLineOptions();
            if (Parser.Default.ParseArguments(args, options))
            {
                if (!client.VerifyUserLogin(options.Login, options.Password))
                {
                    Logger.Fatal("Failed to log in - invalid credentials.");
                    return;
                }

                if (!client.VerifyDeviceId(deviceId))
                    client.AddDevice(options.Login, deviceId, deviceName);

                while (true)
                {
                    if(client.GetShutdownPending(deviceId, options.Login))
                    {
                        client.ClearShutdownPending(deviceId);
                        ShellHandler.Shutdown();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
            else
                Logger.Fatal("Failed to log in - credentials were not specified.");
        }
    }
}
