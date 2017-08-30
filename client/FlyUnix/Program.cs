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
                if (!client.VerifyUserLogin(arguments.Login, arguments.Password).Result)
                    return;

                if (!client.VerifyDeviceId(deviceId).Result)
                {
                    var success = client.AddDevice(arguments.Login, deviceId, deviceName).Result;
                }

                while (true)
                {
                    if(client.GetShutdownPending(deviceId, arguments.Login).Result)
                    {
                        var success = client.ClearShutdownPending(deviceId).Result;
                        ShellHandler.Shutdown();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
