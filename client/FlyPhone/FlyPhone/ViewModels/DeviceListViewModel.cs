using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class DeviceListViewModel : BaseViewModel
    {
        private Command _refreshButtonCommand;
        private Command _logoutButtonCommand;
        private Command _changeFavouriteStateButtonCommand;
        private readonly INavigation _navigation;
        private DeviceCell _selectedItem;
        private string _status;
        public Toggle ToggleBlocks { get; set; } = new Toggle();
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
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public DeviceListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            new Thread(DownloadDevices).Start();
        }

        private async void DownloadDevices()
        {
            Status = "Updating devices list";
            ToggleBlocks.ActivityIndicator = true;

            try
            {
                string username = await RequestHandler.DoRequest(Client.GetUsername(App.Hostname));
                var devicesFromServer = await RequestHandler.DoRequest(Client.GetDevices(username));
                var sortedDevicesFromServer = devicesFromServer.OrderBy(device => DeviceCellConvertor.IsActive(device.LastActive)).ThenBy(device => device.IsFavourite).Reverse();

                Devices.Clear();

                foreach (var device in sortedDevicesFromServer)
                    if (device.IsActionable)
                        Devices.Add(DeviceCellConvertor.Convert(device));
            }
            catch (PhoneRequestException)
            {
                ToggleBlocks.ActivityIndicator = false;
                Status = "Cannot get devices due to network error";
                return;
            }

            Status = string.Empty;
            ToggleBlocks.ActivityIndicator = false;
        }

        private async void ShowDeviceInfoPage(string deviceId)
        {
            Status = string.Empty;
            await _navigation.PushAsync(new DeviceActionPage(deviceId));
        }

        private async void LogoutUser()
        {
            try
            {
                await RequestHandler.DoRequest(Client.SetLoggedState(App.Hostname, false));
            }
            catch (PhoneRequestException)
            {
                Status = "Cannot logout user due to network error";
                return;
            }
            await _navigation.PopModalAsync();
        }

        private void RefreshDevices()
        {
            DownloadDevices();
        }

        public Command LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand ?? (_logoutButtonCommand = new Command(p => LogoutUser(), p => true));
            }
        }

        public Command RefreshButtonCommand
        {
            get
            {
                return _refreshButtonCommand ?? (_refreshButtonCommand = new Command(p => RefreshDevices(), p => true));
            }
        }

        public Command ChangeFavouriteStateButtonCommand => _changeFavouriteStateButtonCommand ?? (_changeFavouriteStateButtonCommand = new Command<string>(ChangeFavouriteState));

        private async void ChangeFavouriteState(string deviceId)
        {
            try
            {
                bool favourite = await RequestHandler.DoRequest(Client.GetFavourite(deviceId));
                if (favourite)
                    await RequestHandler.DoRequest(Client.ClearFavourite(deviceId));
                else
                    await RequestHandler.DoRequest(Client.SetFavourite(deviceId));
            }
            catch (PhoneRequestException)
            {
                Status = "Cannot change favourite state due to network error";
                return;
            }

            new Thread(DownloadDevices).Start();
            Status = string.Empty;
        }
    }
}
