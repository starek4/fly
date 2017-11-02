using FlyPhone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Shared.API.ResponseModels.Device;

namespace FlyPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TablePage
	{
		public TablePage(string login)
		{
			InitializeComponent();
		    BindingContext = new TablePageViewModel(login);
		}

	    private void OnItemTapped(object sender, ItemTappedEventArgs eventArgs)
	    {
            if (eventArgs?.Item is Device device)
	            Navigation.PushAsync(new ShutdownPage(device));
	    }
    }
}