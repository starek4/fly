using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone
{
    // ReSharper disable once RedundantExtendsListEntry
	public partial class App : Application
	{
	    private static string _hostname;
	    
		public App ()
		{
			InitializeComponent();

			MainPage = new LoginViewPage();
		}

	    public static string Hostname => _hostname ?? (_hostname = DependencyService.Get<IDevice>().GetIdentifier());

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
