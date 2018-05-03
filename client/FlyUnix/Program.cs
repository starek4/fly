using System;
using System.Diagnostics;
using System.Threading;
using FlyClientApi;
using FlyUnix.Cli;
using Models;

namespace FlyUnix
{
    static class Program
    {
        private static readonly Client Client = new Client();
        private static Arguments _arguments = new Arguments();
        private static string _deviceId = ShellHandler.GetDeviceId();
        private static string _deviceName = ShellHandler.GetDeviceName();

        static void Main(string[] args)
        {
            if(AlreadyRunning())
            {
                Console.WriteLine("Another instance of fly client is already running.");
                return;
            }

            _deviceId = _deviceId.Replace(Environment.NewLine, String.Empty);
            _deviceName = _deviceName.Replace(Environment.NewLine, String.Empty);

            if (Parser.Parse(args, ref _arguments))
            {
                bool logged = RequestHandler.DoRequest(Client.GetLoggedState(_deviceId));
                if (!logged)
                {
                    _arguments.Password = PasswordGetter.GetPassword();

                    bool isUserVerified = RequestHandler.DoRequest(Client.VerifyUserLoginSecuredPassword(_arguments.Login, _arguments.Password, _deviceId));
                    if (!isUserVerified)
                    {
                        Console.WriteLine("Wrong credentials.");
                        return;
                    }
                }

                Console.WriteLine("Successfully verified. Fly client is now waiting for action request...");

                bool isDeviceVerified = RequestHandler.DoRequest(Client.VerifyDeviceId(_deviceId));

                if (!isDeviceVerified)
                {
                    RequestHandler.DoRequest(() => Client.AddDevice(_arguments.Login, _deviceId, _deviceName, true).Wait());
                    RequestHandler.DoRequest(() => Client.SetLoggedState(_deviceId, true).Wait());
                }

                while (true)
                {
                    RequestHandler.DoRequest(() => Client.UpdateTimestamp(_deviceId).Wait());
                    Device device = RequestHandler.DoRequest(Client.GetDevice(_deviceId));
                    ActionHandler.DoActions(device, Client);
                    Thread.Sleep(QueryTimer.TimeBetweenQuery * 1000);
                }
            }
            Console.WriteLine("Wrong arguments. Try it again: fly -l <login>");
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
