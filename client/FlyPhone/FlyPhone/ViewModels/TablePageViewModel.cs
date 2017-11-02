using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlyPhone.ViewModels.Base;
using Shared.API;
using Device = Shared.API.ResponseModels.Device;

namespace FlyPhone.ViewModels
{
    public class TablePageViewModel : BaseViewModel
    {
        private readonly string _login;
        public TablePageViewModel(string login)
        {
            _login = login;
            GetDevices();
        }
        private readonly Client _client = new Client();
        public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<Device>();

        private async void GetDevices()
        {
            Devices.Clear();
            var devices = new List<Device>(await _client.GetDevices(_login));
            foreach (var device in devices)
            {
                Devices.Add(device);
            }
        }
    }
}
