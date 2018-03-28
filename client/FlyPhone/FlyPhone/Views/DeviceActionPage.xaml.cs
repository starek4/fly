using FlyPhone.ViewModels;
using Xamarin.Forms.Xaml;

namespace FlyPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceActionPage
	{
		public DeviceActionPage ()
		{
			InitializeComponent ();
		    BindingContext = new DeviceActionViewModel();
		}
	}
}