using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private string _status;
        private bool _isEnabledLoginButton = true;
        private Command _loginButtonCommand;
        private readonly INavigation _navigation;
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
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
        public Command LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand ?? (_loginButtonCommand = new Command(p => VerifyUserAndShowDevices(), p => _isEnabledLoginButton));
            }
        }

        private void ChangeLoginButtonState(bool isEnabled)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _isEnabledLoginButton = isEnabled;
                LoginButtonCommand.ChangeCanExecute();
            });
        }

        private async void VerifyUserAndShowDevices()
        {
            ChangeLoginButtonState(false);
            // TODO: verify user
            NavigationPage navPage = new NavigationPage(new DeviceListPage());
            await _navigation.PushModalAsync(navPage);
            ChangeLoginButtonState(true);
        }
    }
}
