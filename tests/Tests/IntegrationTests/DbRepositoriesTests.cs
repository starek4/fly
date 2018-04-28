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
        public void CheckIfUserExist()
        {
            UserRepository userRepo = new UserRepository();
            bool success = userRepo.CheckIfUserExist(TestUserDevice.User.Login);
            Assert.True(success);
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
        public void VerifyUser()
        {
            UserRepository userRepo = new UserRepository();

            bool success = userRepo.VerifyUser(TestUserDevice.User.Login, TestUserDevice.User.Password);

            Assert.True(success);
        }

        [Fact]
        public void VerifyUserWrongPassword()
        {
            UserRepository userRepo = new UserRepository();

            bool success = userRepo.VerifyUser(TestUserDevice.User.Login, TestUserDevice.User.Password + "wrong");

            Assert.False(success);
        }

        [Fact]
        public void AddDeviceToNonExistingUser()
        {
            DeviceRepository deviceRepo = new DeviceRepository();

            bool success = deviceRepo.AddDevice(TestUserDevice.User.Login + "wrong", TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            Assert.False(success);
        }

        [Fact]
        public void AddAlreadyExistingDevice()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            bool success = deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.False(success);
        }

        [Fact]
        public void GetDevices()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            List<Device> devices = new List<Device>(deviceRepo.GetDevicesByLogin(TestUserDevice.User.Login));

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            foreach (Device device in devices)
            {
                if (device.DeviceId == TestUserDevice.Device.DeviceId)
                {
                    Assert.True(true);
                    return;
                }
            }

            Assert.True(false);
        }

        [Fact]
        public void UpdateDevice()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            Device device = deviceRepo.GetDevice(TestUserDevice.Device.DeviceId);
            device.IsShutdownPending = true;
            deviceRepo.UpdateDevice(device);
            device = deviceRepo.GetDevice(TestUserDevice.Device.DeviceId);

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(device.IsShutdownPending);
        }

        [Fact]
        public void GetUsername()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            UserRepository userRepo = new UserRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            User user = userRepo.GetUserByDeviceId(TestUserDevice.Device.DeviceId);

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(user.Login == TestUserDevice.User.Login);
        }

        [Fact]
        public void GetUsernameWrongId()
        {
            DeviceRepository deviceRepo = new DeviceRepository();
            UserRepository userRepo = new UserRepository();
            deviceRepo.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            User user = userRepo.GetUserByDeviceId(TestUserDevice.Device.DeviceId + "wrong");

            deviceRepo.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(user == null);
        }
    }
}
