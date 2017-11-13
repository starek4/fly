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
                bool isUserVerified = RequestHandler.DoRequest(client.VerifyUserLogin(arguments.Login, arguments.Password));
                if (!isUserVerified)
                {
                    return;
                }

                bool isDeviceVerified = RequestHandler.DoRequest(client.VerifyDeviceId(deviceId));

                if (!isDeviceVerified)
                {
                    RequestHandler.DoRequest(() => client.AddDevice(arguments.Login, deviceId, deviceName).Wait());
                }

                while (true)
                {
                    bool isShutdownPending = RequestHandler.DoRequest(client.GetShutdownPending(deviceId));
                    if (isShutdownPending)
                    {
                        RequestHandler.DoRequest(() => client.ClearShutdownPending(deviceId).Wait());
                        ShellHandler.Shutdown();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
