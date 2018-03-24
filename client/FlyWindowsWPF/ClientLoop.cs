using System.Threading;
using System.Threading.Tasks;
using FlyClientApi;
using FlyClientApi.Enums;
using FlyWindowsWPF.PowerShell;
using FlyWindowsWPF.Requests;
using FlyWindowsWPF.TrayIcon;
using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF
{
    public static class ClientLoop
    {
        private static async Task<bool> CheckShutdown(Client client, TrayController controller)
        {
            return await RequestHandler.DoRequest(client.GetAction(DeviceIdentifierHelper.DeviceIdentifier, Actions.Shutdown), controller);
        }

        private static async Task ClearShutdownState(Client client, TrayController controller)
        {
            await RequestHandler.DoRequest(client.ClearAction(DeviceIdentifierHelper.DeviceIdentifier, Actions.Shutdown), controller);
        }

        public static async void Loop(string login, Client client, TrayController controller)
        {
            while (true)
            {
                bool isShutdownPending = await CheckShutdown(client, controller);
                if (isShutdownPending)
                {
                    await ClearShutdownState(client, controller);
                    controller.MakeTooltip("Fly client", "Shutdown request registered.", BalloonIcon.None);
                    ShutdownPc.DoShutdownRequest();
                }
                Thread.Sleep(5 * 1000);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
