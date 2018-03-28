using FlyPhone.ViewModels;

namespace FlyPhone.Views
{
	public partial class DeviceListPage
	{
		public DeviceListPage ()
		{
		    InitializeComponent();
		    BindingContext = new DeviceListViewModel(Navigation);
		}
	}
}