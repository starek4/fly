﻿using System.Threading;
using FlyClientApi;
using FlyWindowsWPF.Action;
using FlyWindowsWPF.PowerShell;
using FlyWindowsWPF.Requests;
using FlyWindowsWPF.TrayIcon;
using Models;

namespace FlyWindowsWPF
{
    public static class ClientLoop
    {
        public static async void Loop(Client client, TrayController controller)
        {
            while (true)
            {
                await RequestHandler.DoRequest(client.UpdateTimestamp(DeviceIdentifierHelper.DeviceIdentifier), controller);
                Device device = await RequestHandler.DoRequest(client.GetDevice(DeviceIdentifierHelper.DeviceIdentifier), controller);
                if (device == null)
                    ErrorHandler.DeletedDevice();
                else
                    ActionHandler.DoActions(device, controller, client);
                Thread.Sleep(QueryTimer.TimeBetweenQuery * 1000);

            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
