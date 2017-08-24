using Shared.API;
using System;
using System.Threading;
using Shared.CLI;
using Shared.Logging;

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
            Arguments arguments = new Arguments();
            if (Parser.Parse(args, ref arguments))
            {
                if (!client.VerifyUserLogin(arguments.Login, arguments.Password))
                {
                    Logger.Fatal("Failed to log in - invalid credentials.");
                    return;
                }

                if (!client.VerifyDeviceId(deviceId))
                    client.AddDevice(arguments.Login, deviceId, deviceName);

                while (true)
                {
                    if(client.GetShutdownPending(deviceId, arguments.Login))
                    {
                        client.ClearShutdownPending(deviceId);
                        PowerShellHandler.ShutdownPc();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
