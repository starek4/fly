using Windows.Security.ExchangeActiveSyncProvisioning;
using FlyPhone.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpDevice))]
namespace FlyPhone.UWP
{
    public class UwpDevice : IDevice
    {
        public string GetIdentifier()
        {
            return new EasClientDeviceInformation().Id.ToString();
        }
    }
}
