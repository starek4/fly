using Android.Provider;
using FlyPhone.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDevice))]
namespace FlyPhone.Droid
{
    public class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
#pragma warning disable 618
            return Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
#pragma warning restore 618
        }
    }
}
