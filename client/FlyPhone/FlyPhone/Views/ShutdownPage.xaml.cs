using FlyPhone.ViewModels;
using Xamarin.Forms.Xaml;

namespace FlyPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShutdownPage
	{
		public ShutdownPage(DeviceCell device)
		{
			InitializeComponent();
		    BindingContext = new ShutdownPageViewModel(Navigation, device);
		}
	}
}