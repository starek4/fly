using FlyApi;
using Xunit;

namespace UnitTests
{
    public class ApiTests
    {
        private string testUsername = "tester";
        private string testDeviceId = "tester";
        private string testDeviceName = "tester";

        private async void AddDevice()
        {
            await new Client().AddDevice(testUsername, testDeviceId, testDeviceName, false);
        }

        private async void DeleteDevice()
        {
            await new Client().DeleteDevice(testUsername, testDeviceId);
        }

        [Fact]
        public void AddingAndDeletingDevice()
        {
            AddDevice();
            DeleteDevice();
        }
    }
}
