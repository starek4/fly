using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlyPhone.Views;
using Xamarin.Forms;
using Device = Models.Device;

namespace FlyPhone.ViewModels
{
    public class DeviceListViewModel : BaseViewModel
    {
        private Command _logoutButtonCommand;
        private readonly INavigation _navigation;
        private DeviceCell _selectedItem;
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
                if (device.IsActionable)
                {
                    Devices.Add(new DeviceCell
                    {
                        DeviceId = device.DeviceId,
                        Name = device.Name,
                        IsActive = DateTime.Now.Subtract(device.LastActive).TotalSeconds < 60,
                        IsFavorite = device.IsFavourite
                    });
                }
            }
        }

        public Command LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand ?? (_logoutButtonCommand = new Command(p => LogoutUser(), p => true));
            }
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
    }

    public class DeviceCell
    {
        public string DeviceId;
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsFavorite { get; set; }

        public bool IsNotActive => !IsActive;
        public bool IsNotFavorite => !IsFavorite;
    }
}
