using Android.App;
using Android.Support.V7.App;

namespace FlyPhone.Droid
{
    [Activity(Label = "Fly client", Icon = "@drawable/shutdown", Theme = "@style/Splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}