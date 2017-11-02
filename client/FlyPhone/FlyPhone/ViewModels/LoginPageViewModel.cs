using System;
using FlyPhone.ViewModels.Base;
using FlyPhone.Views;
using Shared.API;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly Client _client = new Client();
        public string Login { get; set; }
        public string Password { get; set; }
        private string _status;
        private bool _isEnabledLoginButton = true;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }

        private Command _loginButtonCommand;
        public Command LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand ?? (_loginButtonCommand = new Command(p => VerifyUserLogin(), p => _isEnabledLoginButton));
            }
        }

        private readonly INavigation _navigation;

        public LoginPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private async void VerifyUserLogin()
        {
            SetLoginButtonEnabledState(false);
            LoginButtonCommand.ChangeCanExecute();
            if (!await _client.VerifyUserLogin(Login, Password))
            {
                Status = "Wrong username or password";
            }
            else
            {
                Status = String.Empty;
                await _navigation.PushAsync(new TablePage(Login));
            }
            SetLoginButtonEnabledState(true);
        }

        private void SetLoginButtonEnabledState(bool isEnabled)
        {
            _isEnabledLoginButton = isEnabled;
            LoginButtonCommand.ChangeCanExecute();
        }
    }
}
