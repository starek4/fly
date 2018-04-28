using Dapplo.Windows.Input;
using Dapplo.Windows.Input.Enums;

namespace FlyWindowsWPF.Action
{
    public static class AudioHelper
    {
        public static void DoMuteRequest()
        {
            InputGenerator.KeyPress(VirtualKeyCodes.VOLUME_MUTE);
        }
    }
}
