using System.Net;
using Xamarin.Forms;

namespace FlyPhone
{
	public partial class App
	{
	    public static readonly string Hostname = Dns.GetHostName();
        public App()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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
