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
	        if (sender is Button button)
	        {
	            Grid grid = (Grid)button.Parent;
	            foreach (var gridChild in grid.Children)
	            {
	                if (gridChild is Image image)
	                {
	                    await image.FadeTo(0.2, 100);
	                    await image.FadeTo(1, 0);
                    }
	            }
	        }
	    }
    }
}
