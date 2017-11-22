using FlyPhone.ViewModels.Base;
using Shared.API;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class ShutdownPageViewModel : BaseViewModel
    {
        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged(nameof(DeviceName));
            }
        }

        private readonly DeviceCell _device;
        private readonly Client _client = new Client();
        private readonly INavigation _navigation;
        private bool isEnabledShutdownButton = true;
        private Command _shutdownButtonCommand;
        private string _deviceName;

        public ShutdownPageViewModel(INavigation navigation, DeviceCell device)
        {
            _navigation = navigation;
            _device = device;
            DeviceName = device.Name;
        }

        public Command ShutdownButtonCommand
        {
            get
            {
                return _shutdownButtonCommand ?? (_shutdownButtonCommand = new Command(p => ShutdownDevice(), p => isEnabledShutdownButton));
            }
        }

        private async void ShutdownDevice()
        {
            await _client.SetShutdownPending(_device.DeviceId);
            await _navigation.PopAsync(true);
        }
    }
}
