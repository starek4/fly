using System;
using System.Security;
using FlyClientApi;
using FlyClientApi.Enums;
using FlyClientApi.Exceptions;
using Models;
using Xunit;

namespace Tests.IntegrationTests
{
    public class ApiTests
    {

        [Fact]
        public async void AddingUser()
        {
            await Assert.ThrowsAsync<DatabaseException>(() => new Client().AddUser(TestUserDevice.User.Login, TestUserDevice.User.Password, TestUserDevice.User.Email));
        }

        [Fact]
        public async void AddingAndDeletingDevice()
        {
            await new Client().AddDevice(TestUserDevice.Device.Name, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await new Client().DeleteDevice(TestUserDevice.Device.DeviceId);
        }

        [Fact]
        public async void SettingAndGettingDeviceInfo()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.SetLoggedState(TestUserDevice.Device.DeviceId, true);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(device.IsLogged);
        }

        [Fact]
        public async void UpdatingTimeStamp()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.UpdateTimestamp(TestUserDevice.Device.DeviceId);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True((DateTime.Now - device.LastActive).TotalSeconds < 10);
        }

        [Fact]
        public async void SetAction()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.ClearAction(TestUserDevice.Device.DeviceId, Actions.Mute);
            await client.SetAction(TestUserDevice.Device.DeviceId, Actions.Mute);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(device.IsMutePending);
        }

        [Fact]
        public async void GetAction()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            bool action = await client.GetAction(TestUserDevice.Device.DeviceId, Actions.Mute);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.False(action);
        }

        [Fact]
        public async void ClearAction()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.SetAction(TestUserDevice.Device.DeviceId, Actions.Mute);
            await client.ClearAction(TestUserDevice.Device.DeviceId, Actions.Mute);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(device.IsMutePending == false);
        }

        [Fact]
        public async void VerifyUser()
        {
            bool verified = await new Client().VerifyUserLogin(TestUserDevice.User.Login, TestUserDevice.User.Password, TestUserDevice.Device.DeviceId);
            Assert.True(verified);
        }

        [Fact]
        public async void VerifyUserSecuredPassword()
        {
            SecureString pass = new SecureString();
            foreach (char c in TestUserDevice.User.Password)
            {
                pass.AppendChar(c);
            }
            bool verified = await new Client().VerifyUserLoginSecuredPassword(TestUserDevice.User.Login, pass, TestUserDevice.Device.DeviceId);
            Assert.True(verified);
        }

        [Fact]
        public async void GetUserDevices()
        {
            Client client = new Client();
            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            var devices = await client.GetDevices(TestUserDevice.User.Login);
            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(devices.Count == 1 && devices[0].DeviceId == TestUserDevice.Device.DeviceId);
        }

        [Fact]
        public async void SetFavourite()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.SetFavourite(TestUserDevice.Device.DeviceId);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(device.IsFavourite);
        }

        [Fact]
        public async void GetFavourite()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            bool favourite = await client.GetFavourite(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.False(favourite);
        }

        [Fact]
        public async void ClearFavourite()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.SetFavourite(TestUserDevice.Device.DeviceId);
            await client.ClearFavourite(TestUserDevice.Device.DeviceId);

            Device device = await client.GetDevice(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.False(device.IsFavourite);
        }

        [Fact]
        public async void SetGetLoggedState()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            await client.SetLoggedState(TestUserDevice.Device.DeviceId, true);

            bool loggedState = await client.GetLoggedState(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(loggedState);
        }

        [Fact]
        public async void VerifyDeviceId()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);
            bool verified = await client.VerifyDeviceId(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(verified);
        }

        [Fact]
        public async void GetUsername()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            string username = await client.GetUsername(TestUserDevice.Device.DeviceId);

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(username == TestUserDevice.User.Login);
        }

        [Fact]
        public async void GetUsernameWrong()
        {
            Client client = new Client();

            await client.AddDevice(TestUserDevice.User.Login, TestUserDevice.Device.DeviceId, TestUserDevice.Device.Name, false);

            string username = await client.GetUsername(TestUserDevice.Device.DeviceId + "wrong");

            await client.DeleteDevice(TestUserDevice.Device.DeviceId);

            Assert.True(username == null);
        }
    }
}
