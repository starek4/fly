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
        private bool _isEnabledShutdownButton = true;
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
                return _shutdownButtonCommand ?? (_shutdownButtonCommand = new Command(p => ShutdownDevice(), p => _isEnabledShutdownButton));
            }
        }

        private async void ShutdownDevice()
        {
            SetLoginButtonEnabledState(false);
            await RequestHandler.DoRequest(_client.SetShutdownPending(_device.DeviceId));
            SetLoginButtonEnabledState(true);
            await _navigation.PopAsync(true);
        }

        private void SetLoginButtonEnabledState(bool isEnabled)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _isEnabledShutdownButton = isEnabled;
                ShutdownButtonCommand.ChangeCanExecute();
            });
        }
    }
}
