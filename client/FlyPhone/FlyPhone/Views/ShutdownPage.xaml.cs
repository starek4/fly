using FlyPhone.ViewModels;
using Shared.API.ResponseModels;
using Xamarin.Forms.Xaml;

namespace FlyPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShutdownPage
	{
		public ShutdownPage(Device device)
		{
			InitializeComponent();
		    BindingContext = new ShutdownPageViewModel(Navigation, device);
		}
	}
}