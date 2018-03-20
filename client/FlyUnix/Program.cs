using System;
using System.Diagnostics;
using System.Threading;
using FlyApi;
using FlyApi.Enums;
using FlyApi.ResponseModels;
using FlyUnix.Cli;

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
                GetLoggedStateResponse logged = RequestHandler.DoRequest(Client.GetLoggedState(_deviceId));
                if (!logged.Logged)
                {
                    _arguments.Password = PasswordGetter.GetPassword();

                    bool isUserVerified = RequestHandler.DoRequest(Client.VerifyUserLoginSecuredPassword(_arguments.Login, _arguments.Password));
                    if (!isUserVerified)
                    {
                        Console.WriteLine("Wrong credentials.");
                        return;
                    }
                    RequestHandler.DoRequest(() => Client.SetLoggedState(_deviceId).Wait());
                }

                Console.WriteLine("Successfully verified. Fly client is now waiting for action request...");

                bool isDeviceVerified = RequestHandler.DoRequest(Client.VerifyDeviceId(_deviceId));

                if (!isDeviceVerified)
                {
                    RequestHandler.DoRequest(() => Client.AddDevice(_arguments.Login, _deviceId, _deviceName, true).Wait());
                }

                while (true)
                {
                    bool isShutdownPending = RequestHandler.DoRequest(Client.GetAction(_deviceId, ApiAction.Shutdown));
                    if (isShutdownPending)
                    {
                        RequestHandler.DoRequest(() => Client.ClearAction(_deviceId, ApiAction.Shutdown).Wait());
                        ShellHandler.Shutdown();
                        return;
                    }
                    Thread.Sleep(30 * 1000);
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
