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
        public Toggle ToggleBlocks { get; } = new Toggle();
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
            ToggleBlocks.ActivityIndicator = true;

            bool loggedState;
            try
            {
                loggedState = await RequestHandler.DoRequest(Client.GetLoggedState(App.Hostname));
            }
            catch (PhoneRequestException)
            {
                ChangeLoginButtonState(true);
                ToggleBlocks.ActivityIndicator = false;
                Status = "Network error";
                return;
            }

            if (!loggedState)
            {
                ChangeLoginButtonState(true);
                ToggleBlocks.ActivityIndicator = false;
            }
            else
            {
                Device.BeginInvokeOnMainThread(ShowUserDevices);
            }
            Status = String.Empty;
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
            Status = String.Empty;
            ChangeLoginButtonState(false);
            ToggleBlocks.ActivityIndicator = true;

            // Verify login
            try
            {
                bool userVerified = await RequestHandler.DoRequest(Client.VerifyUserLogin(Username, Password));
                if (!userVerified)
                {
                    Status = "Wrong username or password";
                    return;
                }
            }
            catch (PhoneRequestException)
            {
                Status = "Cannot verify user due to network error";
                return;
            }
            finally
            {
                ToggleBlocks.ActivityIndicator = false;
                ChangeLoginButtonState(true);
            }

            // Check if device already exist
            ToggleBlocks.ActivityIndicator = true;
            Status = "Trying to login";
            try
            {
                bool deviceVerified = await RequestHandler.DoRequest(Client.VerifyDeviceId(App.Hostname));
                if (!deviceVerified)
                {
                    await RequestHandler.DoRequest(Client.AddDevice(Username, App.Hostname, App.Hostname, false));
                }

                await RequestHandler.DoRequest(Client.SetLoggedState(App.Hostname, true));
            }
            catch (PhoneRequestException)
            {
                Status = "Cannot verify device due to network error";
                return;
            }
            finally
            {
                ToggleBlocks.ActivityIndicator = false;
                ChangeLoginButtonState(true);
            }
            ShowUserDevices();
        }

        private async void ShowUserDevices()
        {
            NavigationPage navPage = new NavigationPage(new DeviceListPage());
            await _navigation.PushModalAsync(navPage);
            ChangeLoginButtonState(true);
            ToggleBlocks.ActivityIndicator = false;
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
