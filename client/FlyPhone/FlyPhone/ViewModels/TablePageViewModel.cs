using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlyPhone.ViewModels.Base;
using Shared.API;
using Xamarin.Forms;
using Device = Shared.API.ResponseModels.Device;

namespace FlyPhone.ViewModels
{
    public class TablePageViewModel : BaseViewModel
    {
        private readonly string _login;
        private readonly INavigation _navigation;

        public TablePageViewModel(string login, INavigation navigation)
        {
            _navigation = navigation;
            _login = login;
            GetDevices();
        }

        private readonly Client _client = new Client();
        public ObservableCollection<DeviceCell> Devices { get; set; } = new ObservableCollection<DeviceCell>();

        private async void GetDevices()
        {
            var devices = new List<Device>(await RequestHandler.DoRequest(_client.GetDevices(_login)));
            Devices.Clear();
            var green = ImageSource.FromFile("green.png");
            var red = ImageSource.FromFile("red.png");
            foreach (var device in devices)
            {
                Devices.Add((DateTime.Now - device.LastActive).TotalSeconds < 60
                    ? new DeviceCell {Name = device.Name, DeviceId = device.DeviceId, Image = green}
                    : new DeviceCell {Name = device.Name, DeviceId = device.DeviceId, Image = red});
            }
        }

    private bool _isEnabledLogoutButton = true;
        private Command _logoutButtonCommand;
        public Command LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand ?? (_logoutButtonCommand = new Command(p => Logout(), p => _isEnabledLogoutButton));
            }
        }

        private async void Logout()
        {
            await RequestHandler.DoRequest(_client.ClearLoggedState(App.Hostname));
            await _navigation.PopModalAsync();
        }
    }
}
