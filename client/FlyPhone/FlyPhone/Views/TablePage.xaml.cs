﻿using FlyPhone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TablePage
	{
		public TablePage(string login)
		{
			InitializeComponent();
		    BindingContext = new TablePageViewModel(login, Navigation);
		}

	    private void OnItemTapped(object sender, ItemTappedEventArgs eventArgs)
	    {
	        if (eventArgs?.Item is DeviceCell device)
	        {
	            Navigation.PushModalAsync(new ShutdownPage(device));

                // Try to disable higlighting
                if (sender is ListView list)
	                list.SelectedItem = null;
            }
	            
	    }
    }
}
