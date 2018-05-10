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
	        Image image = null;

	        if (sender is Button button)    // UWP - button is under the image in GRID
            {
	            Grid grid = (Grid)button.Parent;
	            foreach (var gridChild in grid.Children)
	            {
	                if (gridChild is Image imageChild)
	                {
	                    image = imageChild;
	                    await image.FadeTo(0.2, 100);
	                    await image.FadeTo(1, 0);
                    }
	            }
	        }
            else if (sender is Image imageSender)   // Android, iOS - image has tap event
            {
	            image = imageSender;
	        }

	        if (image != null)
	        {
	            await image.FadeTo(0.2, 100);
	            await image.FadeTo(1, 0);
            }
	    }
    }
}
