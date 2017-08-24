using Shared.API;
using System;
using System.Threading;
using CommandLine;
using Shared.Logger;

namespace FlyWindows
{
    class Program
    {
        static void Main(string[] args)
        {
            string deviceId = PowerShellHandler.GetUUID();
            string deviceName = PowerShellHandler.GetDeviceName();

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
                        Logger.Info("Received shutdown message.");
                        client.ClearShutdownPending(deviceId);
                        PowerShellHandler.ShutdownPc();
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
