using System.Net;
using Xamarin.Forms;

namespace FlyPhone
{
	public partial class App
	{
	    public static string Hostname;

        public App()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

		    IDevice device = DependencyService.Get<IDevice>();
		    Hostname = device.GetIdentifier();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
