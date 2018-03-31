using System.Collections.Generic;
using DatabaseController.Repositories;
using Models;
using Xunit;

namespace Tests.IntegrationTests
{
    public class DbRepositoriesTests
    {
        [Fact]
        public void AddUser()
        {
            UserRepository userRepo = new UserRepository();
            bool success = userRepo.AddUser(TestUserDevice.User.Login, TestUserDevice.User.Password, TestUserDevice.User.Email);
            Assert.False(success);
        }

        [Fact]
        public void DeleteAndAddTestUser()
        {
            UserRepository userRepo = new UserRepository();

            userRepo.DeleteUser(TestUserDevice.User.Login);
            bool success = userRepo.AddUser(TestUserDevice.User.Login, TestUserDevice.User.Password, TestUserDevice.User.Email);
            
            Assert.True(success);
        }

        [Fact]
        public void GetDevices()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            List<Device> devices = new List<Device>(deviceRepo.GetDevicesByLogin(TestUserDevice.User.Login));

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(devices.Count == 1 && devices[0].DeviceId == TestUserDevice.Device.DeviceId);
        }
    }
}
