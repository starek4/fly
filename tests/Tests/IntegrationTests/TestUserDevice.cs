using System.Collections.Generic;
using Models;

namespace Tests.IntegrationTests
{
    public static class TestUserDevice
    {
        public static Device Device { get; } = new Device {DeviceId = "tester", Name = "tester" };

        public static User User { get; } = new User {Email = "tester", Login = "tester", Password = "tester", Devices = new List<Device> { Device }};
    }
}
