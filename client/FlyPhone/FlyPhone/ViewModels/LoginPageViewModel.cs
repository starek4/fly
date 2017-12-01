﻿using System;
using System.Threading;
using FlyApi;
using FlyApi.ResponseModels;
using FlyPhone.ViewModels.Base;
using FlyPhone.Views;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace FlyPhone.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly Client _client = new Client();
        public string Login { get; set; }
        public string Password { get; set; }
        private string _status;
        private bool _isEnabledLoginButton = true;
        private readonly string _hostname = App.Hostname();

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
            Status = "Checking logging status...";
            new Thread(IsDeviceLogged).Start();
        }

        private async void IsDeviceLogged()
        {
            SetLoginButtonEnabledState(false);

            GetLoggedStateResponse logged = await RequestHandler.DoRequest(_client.GetLoggedState(_hostname));
            if (logged.Logged)
            {
                Status = String.Empty;
                SetLoginButtonEnabledState(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _navigation.PushModalAsync(new NavigationPage(new TablePage(logged.Login)));
                });
            }
            else
            {
                Status = String.Empty;
                SetLoginButtonEnabledState(true);
            }
        }

        private async void VerifyUserLogin()
        {
            SetLoginButtonEnabledState(false);
            if (!await RequestHandler.DoRequest(_client.VerifyUserLogin(Login, Password)))
            {
                Status = "Wrong username or password";
            }
            else
            {
                bool isDeviceRegistered = await RequestHandler.DoRequest(_client.VerifyDeviceId(_hostname));
                if (isDeviceRegistered == false)
                    await RequestHandler.DoRequest(_client.AddDevice(Login, _hostname, _hostname, false));
                await RequestHandler.DoRequest(_client.SetLoggedState(_hostname));
                Status = String.Empty;
                await _navigation.PushModalAsync(new NavigationPage(new TablePage(Login)));
            }
            SetLoginButtonEnabledState(true);
        }

        private void SetLoginButtonEnabledState(bool isEnabled)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _isEnabledLoginButton = isEnabled;
                LoginButtonCommand.ChangeCanExecute();
            });
        }
    }
}
