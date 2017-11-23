using Android.Provider;
using FlyPhone.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]
namespace FlyPhone.Droid
{
    public class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
            return Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}