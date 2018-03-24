using System;
using DatabaseController.Models;
using FlyClientApi.Enums;

namespace FlyClientApi.Mappers
{
    public static class ActionMapper
    {
        public static Device ActionSet(Actions action, Device device, bool boolean)
        {
            switch (action)
            {
                case Actions.Shutdown:
                    device.IsShutdownPending = boolean;
                    break;
                case Actions.Restart:
                    device.IsRestartPending = boolean;
                    break;
                case Actions.Sleep:
                    device.IsSleepPending = boolean;
                    break;
                case Actions.Mute:
                    device.IsMutePending = boolean;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return device;
        }

        public static bool ActionGet(Actions action, Device device)
        {
            switch (action)
            {
                case Actions.Shutdown:
                    return device.IsShutdownPending;
                case Actions.Restart:
                    return device.IsRestartPending;
                case Actions.Sleep:
                    return device.IsSleepPending;
                case Actions.Mute:
                    return device.IsMutePending;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
