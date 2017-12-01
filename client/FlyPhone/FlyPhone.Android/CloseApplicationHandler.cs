using Android.App;
using FlyPhone.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplicationHandler))]
namespace FlyPhone.Droid
{
    public class CloseApplicationHandler : ICloseApplication
    {
        public void CloseApplication()
        {
#pragma warning disable 618
            var activity = (Activity)Forms.Context;
#pragma warning restore 618
            activity.FinishAffinity();
        }
    }
}