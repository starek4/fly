using DatabaseController.Models;
using FlyClientApi;
using FlyClientApi.Enums;
using FlyWindowsWPF.Requests;
using FlyWindowsWPF.TrayIcon;
using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.Action
{
    public static class ActionHandler
    {
        public static async void DoActions(Device device, TrayController controller, Client client)
        {
            if (device.IsShutdownPending)
            {
                await RequestHandler.DoRequest(client.ClearAction(device.DeviceId, Actions.Shutdown), controller);
                controller.MakeTooltip("Fly client", "Shutdown request registered.", BalloonIcon.None);
                ShutdownHelper.DoShutdownRequest();
            }
            else if (device.IsRestartPending)
            {
                await RequestHandler.DoRequest(client.ClearAction(device.DeviceId, Actions.Restart), controller);
                controller.MakeTooltip("Fly client", "Restart request registered.", BalloonIcon.None);
                ShutdownHelper.DoRestartRequest();
            }
            else if (device.IsSleepPending)
            {
                await RequestHandler.DoRequest(client.ClearAction(device.DeviceId, Actions.Sleep), controller);
                controller.MakeTooltip("Fly client", "Sleep request registered.", BalloonIcon.None);
                ShutdownHelper.DoSleepRequest();
            }
            else if (device.IsMutePending)
            {
                await RequestHandler.DoRequest(client.ClearAction(device.DeviceId, Actions.Mute), controller);
                controller.MakeTooltip("Fly client", "Mute request registered.", BalloonIcon.None);
                AudioHelper.DoMuteRequest();
            }
        }
    }
}
