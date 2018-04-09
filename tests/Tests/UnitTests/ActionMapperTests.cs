using FlyClientApi.Enums;
using FlyClientApi.Mappers;
using Models;
using Xunit;

namespace Tests.UnitTests
{
    public class ActionMapperTests
    {
        [Fact]
        public void SetGetShutdownActionTest()
        {
            Device device = new Device();
            ActionMapper.ActionSet(Actions.Shutdown, device, true);
            bool test = ActionMapper.ActionGet(Actions.Shutdown, device);
            Assert.True(test);
        }
        [Fact]
        public void SetGetRestartActionTest()
        {
            Device device = new Device();
            ActionMapper.ActionSet(Actions.Restart, device, true);
            bool test = ActionMapper.ActionGet(Actions.Restart, device);
            Assert.True(test);
        }
        [Fact]
        public void SetGetSleepActionTest()
        {
            Device device = new Device();
            ActionMapper.ActionSet(Actions.Sleep, device, true);
            bool test = ActionMapper.ActionGet(Actions.Sleep, device);
            Assert.True(test);
        }
        [Fact]
        public void SetGetMuteActionTest()
        {
            Device device = new Device();
            ActionMapper.ActionSet(Actions.Mute, device, true);
            bool test = ActionMapper.ActionGet(Actions.Mute, device);
            Assert.True(test);
        }
    }
}
