using System;
using FlyClientApi;
using FlyClientApi.Enums;
using Models;
using Xunit;

namespace UnitTests
{
    public class ApiTests
    {
        private string testUsername = "tester";
        private string testDeviceId = "tester";
        private string testDeviceName = "tester";

        [Fact]
        public async void AddingAndDeletingDevice()
        {
            await new Client().AddDevice(testDeviceName, testDeviceId, testDeviceName, false);
            await new Client().DeleteDevice(testDeviceId);
        }

        [Fact]
        public async void SettingAndGettingDeviceInfo()
        {
            Client client = new Client();

            await client.AddDevice(testUsername, testDeviceId, testDeviceName, false);
            await client.SetLoggedState(testDeviceId, true);

            Device device = await client.GetDevice(testDeviceId);
            Assert.True(device.IsLogged);

            await client.DeleteDevice(testDeviceId);
        }

        [Fact]
        public async void UpdatingTimeStamp()
        {
            Client client = new Client();

            await client.AddDevice(testUsername, testDeviceId, testDeviceName, false);
            await client.UpdateTimestamp(testDeviceId);

            Device device = await client.GetDevice(testDeviceId);
            Assert.True((DateTime.Now - device.LastActive).TotalSeconds < 10);

            await client.DeleteDevice(testDeviceId);
        }

        [Fact]
        public async void SetAction()
        {
            Client client = new Client();

            await client.AddDevice(testUsername, testDeviceId, testDeviceName, false);
            await client.ClearAction(testDeviceId, Actions.Mute);
            await client.SetAction(testDeviceId, Actions.Mute);

            Device device = await client.GetDevice(testDeviceId);
            Assert.True(device.IsMutePending);

            await client.DeleteDevice(testDeviceId);
        }

        [Fact]
        public async void ClearAction()
        {
            Client client = new Client();

            await client.AddDevice(testUsername, testDeviceId, testDeviceName, false);
            await client.SetAction(testDeviceId, Actions.Mute);
            await client.ClearAction(testDeviceId, Actions.Mute);

            Device device = await client.GetDevice(testDeviceId);
            Assert.True(device.IsMutePending == false);

            await client.DeleteDevice(testDeviceId);
        }
    }
}
