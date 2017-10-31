using System.Threading;
using System.Threading.Tasks;
using FlyWindowsWPF.PowerShell;
using FlyWindowsWPF.Requests;
using FlyWindowsWPF.TrayIcon;
using Hardcodet.Wpf.TaskbarNotification;
using Shared.API;

namespace FlyWindowsWPF
{
    public static class ClientLoop
    {
        private static async Task<bool> CheckShutdown(Client client, string login)
        {
            return await RequestHandler.DoRequest(client.GetShutdownPending(DeviceIdentifierHelper.DeviceIdentifier, login));
        }

        private static async Task ClearShutdownState(Client client)
        {
            await RequestHandler.DoRequest(client.ClearShutdownPending(DeviceIdentifierHelper.DeviceIdentifier));
        }

        public static async void Loop(Client client, TrayController controller, string login)
        {
            while (true)
            {
                bool isShutdownPending = await CheckShutdown(client, login);
                if (await CheckShutdown(client, login))
                {
                    await ClearShutdownState(client);
                    controller.MakeTooltip("Fly client", "Shutdown request registered.", BalloonIcon.None);
                    ShutdownPc.DoShutdownRequest();
                    return;
                }
                Thread.Sleep(30 * 1000);
            }
        }
    }
}
