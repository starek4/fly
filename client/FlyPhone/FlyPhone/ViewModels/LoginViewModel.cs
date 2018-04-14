using System;
using System.Threading;
using FlyPhone.Views;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private string _status;
        private bool _isEnabledLoginButton;
        private Command _loginButtonCommand;
        private readonly INavigation _navigation;
        private bool _isBusy = true;
        private bool _loginVisibility = false;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }
        public bool LoginVisibility
        {
            get => !_isBusy;
            set
            {
                _loginVisibility = value;
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            new Thread(TryToLoginUser).Start();
        }

        private async void TryToLoginUser()
        {
            Status = "Trying to log in";
            IsBusy = true;
            if (!await RequestHandler.DoRequest(Client.GetLoggedState(App.Hostname)))
            {
                ChangeLoginButtonState(true);
                IsBusy = false;
                Status = String.Empty;
            }
            else
            {
                Device.BeginInvokeOnMainThread(ShowUserDevices);
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
            IsBusy = true;
            // Verify login
            if (!await RequestHandler.DoRequest(Client.VerifyUserLogin(Username, Password)))
            {
                Status = "Wrong username or password";
                IsBusy = false;
                ChangeLoginButtonState(true);
                return;
            }

            // Check if device already exist
            IsBusy = true;
            Status = "Trying to login";
            if (!await RequestHandler.DoRequest(Client.VerifyDeviceId(App.Hostname)))
            {
                await RequestHandler.DoRequest(Client.AddDevice(Username, App.Hostname, App.Hostname, false));
            }
            await RequestHandler.DoRequest(Client.SetLoggedState(App.Hostname, true));
            
            ShowUserDevices();
        }

        private async void ShowUserDevices()
        {
            NavigationPage navPage = new NavigationPage(new DeviceListPage());
            await _navigation.PushModalAsync(navPage);
            ChangeLoginButtonState(true);
            IsBusy = false;
            Status = String.Empty;
        }

        public Command LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand ?? (_loginButtonCommand = new Command(p => VerifyUserAndShowDevices(), p => _isEnabledLoginButton));
            }
        }
    }
}
