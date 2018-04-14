using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FlyPhone.Views;
using Xamarin.Forms;
using Device = Models.Device;

namespace FlyPhone.ViewModels
{
    public class DeviceListViewModel : BaseViewModel
    {
        private Command _logoutButtonCommand;
        private Command _showDeviceInfoCommand;
        private readonly INavigation _navigation;
        public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<Device>();


        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public DeviceListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            DownloadDevices();
        }

        private void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }

        private async void DownloadDevices()
        {
            string username = await RequestHandler.DoRequest(Client.GetUsername(App.Hostname));
            List<Device> devicesFromServer = await RequestHandler.DoRequest(Client.GetDevices(username));

            foreach (var device in devicesFromServer)
            {
                Devices.Add(device);
            }
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
            await RequestHandler.DoRequest(Client.SetLoggedState(App.Hostname, false));
            await _navigation.PopModalAsync();
        }
    }
}
