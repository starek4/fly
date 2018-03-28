using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class DeviceListViewModel : BaseViewModel
    {
        private Command _logoutButtonCommand;
        private Command _showDeviceInfoCommand;
        private readonly INavigation _navigation;
        public DeviceListViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
        public Command LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand ?? (_logoutButtonCommand = new Command(p => LogoutUser(), p => true));
            }
        }

        public Command ShowDeviceInfoCommand
        {
            get
            {
                return _showDeviceInfoCommand ?? (_showDeviceInfoCommand = new Command(p => ShowDeviceInfoPage(), p => true));
            }
        }

        private async void ShowDeviceInfoPage()
        {
            await _navigation.PushAsync(new DeviceActionPage());
        }

        private async void LogoutUser()
        {
            await _navigation.PopModalAsync();
        }
    }
}
