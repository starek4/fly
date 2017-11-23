using Android.App;
using FlyPhone.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace FlyPhone.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}