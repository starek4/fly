using System;
using System.Threading;
using Shared.API;
using CommandLine;
using Shared.Logger;

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
                    Logger.Fatal("Credentials were wrong.");
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
                Logger.Fatal("Credentials were not specified.");
        }
    }
}
