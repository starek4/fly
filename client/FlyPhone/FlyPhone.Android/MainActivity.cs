using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Toasts;
using Xamarin.Forms;

namespace FlyPhone.Droid
{
	[Activity (Label = "FlyPhone", Icon = "@drawable/shutdown", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			Forms.Init (this, bundle);

            // Register and init toast notification
		    DependencyService.Register<ToastNotification>();
		    ToastNotification.Init(this);

            LoadApplication (new App());
		}
	}
}

