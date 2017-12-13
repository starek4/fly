using Xamarin.Forms;

namespace FlyPhone
{
	public partial class App
	{
        public App()
		{
			InitializeComponent();

            MainPage = new LoginPage();
        }

	    public static string Hostname()
	    {
	        IDevice device = DependencyService.Get<IDevice>();
	        return device.GetIdentifier();
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
