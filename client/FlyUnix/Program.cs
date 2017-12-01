using System;
using System.Diagnostics;
using System.Threading;
using FlyApi;
using FlyUnix.Cli;

namespace FlyUnix
{
    static class Program
    {
        static void Main(string[] args)
        {
            if(AlreadyRunning())
            {
                Console.WriteLine("Another instance of fly client is already running.");
                return;
            }

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
                    RequestHandler.DoRequest(() => client.AddDevice(arguments.Login, deviceId, deviceName, true).Wait());
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
            Console.WriteLine("Wrong arguments. Try it again: fly -l <login> -p <password>");
        }

        private static bool AlreadyRunning()
        {
            Process[] processes = Process.GetProcesses();
            Process currentProc = Process.GetCurrentProcess();
            foreach (Process process in processes)
            {
                if (currentProc.ProcessName == process.ProcessName && currentProc.Id != process.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
