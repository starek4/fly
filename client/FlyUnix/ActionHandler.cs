using System;
using System.Threading.Tasks;
using FlyClientApi;
using FlyClientApi.Enums;
using Models;

namespace FlyUnix
{
    public static class ActionHandler
    {
        public static void DoActions(Device device, Client client)
        {
            if (device.IsShutdownPending)
            {
                RequestHandler.DoRequest(() => client.ClearAction(device.DeviceId, Actions.Shutdown).Wait());
                ShellHandler.Shutdown();
            }
            else if (device.IsRestartPending)
            {
                RequestHandler.DoRequest(() => client.ClearAction(device.DeviceId, Actions.Restart).Wait());
                ShellHandler.Restart();
            }
            else if (device.IsSleepPending)
            {
                RequestHandler.DoRequest(() => client.ClearAction(device.DeviceId, Actions.Sleep).Wait());
                ShellHandler.Sleep();
            }
            else if (device.IsMutePending)
            {
                RequestHandler.DoRequest(() => client.ClearAction(device.DeviceId, Actions.Mute).Wait());
                ShellHandler.Mute();
            }
        }

        private static async Task DoActionAfterOneSecond(Action action)
        {
            await Task.Delay(1000);
            action.Invoke();
        }
    }
}
