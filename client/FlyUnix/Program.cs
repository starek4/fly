using System;
using System.Threading;
using Shared.API;
using Shared.CLI;

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
            Arguments arguments = new Arguments();
            if (Parser.Parse(args, ref arguments))
            {
                if (!client.VerifyUserLogin(arguments.Login, arguments.Password))
                    return;

                if (!client.VerifyDeviceId(deviceId))
                    client.AddDevice(arguments.Login, deviceId, deviceName);

                while (true)
                {
                    if(client.GetShutdownPending(deviceId, arguments.Login))
                    {
                        client.ClearShutdownPending(deviceId);
                        ShellHandler.Shutdown();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
