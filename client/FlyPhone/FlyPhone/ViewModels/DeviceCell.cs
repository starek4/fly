using System;
using Models;

namespace FlyPhone.ViewModels
{
    public class DeviceCell
    {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsFavourite { get; set; }

        public bool IsNotActive => !IsActive;
        public bool IsNotFavourite => !IsFavourite;
    }

    public static class DeviceCellConvertor
    {
        public static DeviceCell Convert(Device device)
        {
            return new DeviceCell
            {
                DeviceId = device.DeviceId,
                Name = device.Name,
                IsActive = IsActive(device.LastActive),
                IsFavourite = device.IsFavourite
            };
        }

        public static bool IsActive(DateTime lastActive)
        {
            return DateTime.Now.Subtract(lastActive).TotalSeconds < 60;
        }
    }
}