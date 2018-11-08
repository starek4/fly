using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseController.Repositories
{
    public class DeviceRepository
    {
        public DeviceRepository()
        {
            using (var context = new FlyDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
        private User FindUser(string login, FlyDbContext context)
        {
            return context.Users.Include(x => x.Devices).FirstOrDefault(x => x.Login == login);
        }

        public bool AddDevice(string login, string deviceId, string deviceName, bool actionable)
        {
            using (var context = new FlyDbContext())
            {
                var user = FindUser(login, context);
                if (user == null)
                    return false;

                if (user.Devices.Any(x => x.DeviceId == deviceId))
                    return false;

                user.Devices.Add(new Device {DeviceId = deviceId, Name = deviceName, IsActionable = actionable});
                context.SaveChanges();
            }
            return true;
        }

        public bool ChangeOwnerOfDevice(string newOwnerLogin, string deviceId)
        {
            using (var context = new FlyDbContext())
            {
                // Find out user by login
                var userByLogin = FindUser(newOwnerLogin, context);
                if (userByLogin == null)
                    return false;

                // Find out user by deviceId
                User userByDeviceId = null;
                foreach (User user in context.Users.Include(x => x.Devices))
                {
                    foreach (Device device in user.Devices)
                    {
                        if (device.DeviceId == deviceId)
                            userByDeviceId = user;
                    }
                }
                if (userByDeviceId == null)
                    return false;

                // Find out device by deviceId
                Device deviceByDeviceId = context.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
                if (deviceByDeviceId == null)
                    return false;

                // Remove device from userByDeviceId
                context.Devices.Remove(deviceByDeviceId);
                context.SaveChanges();

                // Add device to userByLogin
                userByLogin.Devices.Add(deviceByDeviceId);
                context.SaveChanges();
            }
            return true;
        }

        public bool DeleteDevice(string deviceId)
        {
            using (var context = new FlyDbContext())
            {
                var device = context.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
                if (device == null)
                    return false;

                context.Devices.Remove(device);
                context.SaveChanges();
            }
            return true;
        }

        public ICollection<Device> GetDevicesByLogin(string login)
        {
            using (var context = new FlyDbContext())
            {
                return context.Users.Include(x => x.Devices).First(x => x.Login == login).Devices;
            }
        }

        public Device GetDevice(string deviceId)
        {
            using (var context = new FlyDbContext())
            {
                return context.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
            }
        }

        public void UpdateDevice(Device device)
        {
            using (var context = new FlyDbContext())
            {
                Device dbDevice = context.Devices.FirstOrDefault(x => x.DeviceId == device.DeviceId);
                if (dbDevice == null) return;
                dbDevice.IsActionable = device.IsActionable;
                dbDevice.IsFavourite = device.IsFavourite;
                dbDevice.IsLogged = device.IsLogged;
                dbDevice.IsMutePending = device.IsMutePending;
                dbDevice.IsRestartPending = device.IsRestartPending;
                dbDevice.IsShutdownPending = device.IsShutdownPending;
                dbDevice.IsSleepPending = device.IsSleepPending;
                dbDevice.LastActive = device.LastActive;
                dbDevice.Name = device.Name;
                context.SaveChanges();
            }
        }
    }
}
