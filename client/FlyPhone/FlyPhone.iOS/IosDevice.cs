using FlyPhone.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosDevice))]
namespace FlyPhone.iOS
{
    public class IosDevice : IDevice
    {
        public string GetIdentifier()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}
