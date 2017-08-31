using Shared.API;
using System;
using System.Threading;
using Shared.CLI;

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
                bool isUserVerified = RequestHandler.DoRequest(client.VerifyUserLogin(arguments.Login, arguments.Password));
                if (!isUserVerified)
                {
                    return;
                }

                bool isDeviceVerified = RequestHandler.DoRequest(client.VerifyDeviceId(deviceId));

                if (!isDeviceVerified)
                {
                    RequestHandler.DoRequest(() => client.AddDevice(arguments.Login, deviceId, deviceName));
                }

                while (true)
                {
                    bool isShutdownPending = RequestHandler.DoRequest(client.GetShutdownPending(deviceId, arguments.Login));
                    if(isShutdownPending)
                    {
                        RequestHandler.DoRequest(() => client.ClearShutdownPending(deviceId));
                        PowerShellHandler.ShutdownPc();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
