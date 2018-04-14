using System.Collections.ObjectModel;
using System.Linq;
using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class DeviceListViewModel : BaseViewModel
    {
        private Command _logoutButtonCommand;
        private Command _changeFavouriteStateButtonCommand;
        private readonly INavigation _navigation;
        private DeviceCell _selectedItem;
        private bool _isBusy;
        public ObservableCollection<DeviceCell> Devices { get; } = new ObservableCollection<DeviceCell>();

        public DeviceCell SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                ShowDeviceInfoPage(_selectedItem.DeviceId);

                SelectedItem = null;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
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
            var devicesFromServer = await RequestHandler.DoRequest(Client.GetDevices(username));
            var sortedDevicesFromServer = devicesFromServer.OrderBy(device => DeviceCellConvertor.IsActive(device.LastActive)).ThenBy(device => device.IsFavourite).Reverse();

            Devices.Clear();

            foreach (var device in sortedDevicesFromServer)
                if (device.IsActionable)
                    Devices.Add(DeviceCellConvertor.Convert(device));

        }

        private async void ShowDeviceInfoPage(string deviceId)
        {
            await _navigation.PushAsync(new DeviceActionPage(deviceId));
        }

        private async void LogoutUser()
        {
            await RequestHandler.DoRequest(Client.SetLoggedState(App.Hostname, false));
            await _navigation.PopModalAsync();
        }

        public Command LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand ?? (_logoutButtonCommand = new Command(p => LogoutUser(), p => true));
            }
        }

        public Command ChangeFavouriteStateButtonCommand => _changeFavouriteStateButtonCommand ?? (_changeFavouriteStateButtonCommand = new Command<string>(ChangeFavouriteState));

        private async void ChangeFavouriteState(string deviceId)
        {
            bool favourite = await RequestHandler.DoRequest(Client.GetFavourite(deviceId));
            if (favourite)
                await RequestHandler.DoRequest(Client.ClearFavourite(deviceId));
            else
                await RequestHandler.DoRequest(Client.SetFavourite(deviceId));

            DownloadDevices();
        }
    }
}
