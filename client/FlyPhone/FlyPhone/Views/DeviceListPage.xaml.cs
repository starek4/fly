using System;
using FlyPhone.ViewModels;
using Xamarin.Forms;

namespace FlyPhone.Views
{
	public partial class DeviceListPage
	{
		public DeviceListPage ()
		{
		    InitializeComponent();
		    BindingContext = new DeviceListViewModel(Navigation);
		}

	    private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
	        if (sender is Image image)
	        {
	            await image.FadeTo(0.2, 100);
	            await image.FadeTo(1, 0);
	        }
	    }
	}
}
