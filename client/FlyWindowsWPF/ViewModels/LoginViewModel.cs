using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FlyWindowsWPF.PowerShell;
using FlyWindowsWPF.Requests;
using FlyWindowsWPF.TrayIcon;
using FlyWindowsWPF.ViewModels.Base;
using FlyWindowsWPF.ViewModels.Commands;
using Hardcodet.Wpf.TaskbarNotification;
using Shared.API;
using Shared.Enviroment;
using Shared.Logging;

namespace FlyWindowsWPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _password;
        private readonly ILogger _logger = EnviromentHelper.GetLogger();
        private readonly Client _client = new Client();
        private string _status;
        private bool _isEnabledLoginButton;
        public TrayController TrayController { private get; set; }

        public string Login { get; set; }

        public LoginViewModel()
        {
            Status = "Checking logging status...";
            new Thread(IsDeviceLogged).Start();
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        private async void LogoutAndExit()
        {
            await _client.ClearLoggedState(DeviceIdentifierHelper.DeviceIdentifier);
            ExitApp();
        }

        private void LoginAction(object parameter)
        {
            DisableLoginButton();
            if (parameter is PasswordBox passwordContainer)
            {
                _password = passwordContainer.Password;
            }
            else
            {
                _logger.Fatal("Wrong command argument provided.");
                throw new NotImplementedException("Wrong command argument.");
            }

            new Thread(async () => await ProcessLogin()).Start();
        }
        
        private async Task ProcessLogin()
        {
            bool isUserVerified = await RequestHandler.DoRequest(_client.VerifyUserLogin(Login, _password));
            Status = isUserVerified ? "Login successfully verified." : "Login denied!";
            if (isUserVerified)
            {
                bool isDeviceRegistered = await RequestHandler.DoRequest(_client.VerifyDeviceId(DeviceIdentifierHelper.DeviceIdentifier));
                if (isDeviceRegistered == false)
                    await RequestHandler.DoRequest(_client.AddDevice(Login, DeviceIdentifierHelper.DeviceIdentifier, DeviceNameHelper.DeviceName));
                IntoTray();
            }
            else
            {
                EnableLoginButton();
            }
        }

        private async void IntoTray()
        {
            await RequestHandler.DoRequest(_client.SetLoggedState(DeviceIdentifierHelper.DeviceIdentifier));
            Status = String.Empty;
            HideWindow();
            new Thread(() => ClientLoop.Loop(_client, TrayController, Login)).Start();
        }

        private async void IsDeviceLogged()
        {
            bool logged = await RequestHandler.DoRequest(_client.GetLoggedState(DeviceIdentifierHelper.DeviceIdentifier));
            if (logged)
                IntoTray();
            else
            {
                Status = String.Empty;
                EnableLoginButton();
            }
        }

        private void EnableLoginButton()
        {
            _isEnabledLoginButton = true;
            LoginButtonCommand.Refresh();
        }

        private void DisableLoginButton()
        {
            _isEnabledLoginButton = false;
            LoginButtonCommand.Refresh();
        }

        private void HideWindow()
        {
            IsVisible = Visibility.Hidden;
            TrayController.MakeTooltip("Fly client", "Running in background...", BalloonIcon.None);
        }

        private RelayCommand _exitAppCommand;
        public RelayCommand ExitAppCommand
        {
            get
            {
                return _exitAppCommand ?? (_exitAppCommand = new RelayCommand(
                           p => true,
                           p => ExitApp()));
            }
        }

        private RelayCommand _logoutAndExitCommand;
        public RelayCommand LogoutAndExitCommand
        {
            get
            {
                return _logoutAndExitCommand ?? (_logoutAndExitCommand = new RelayCommand(
                           p => true,
                           p => LogoutAndExit()));
            }
        }

        private RelayCommand _loginButtonCommand;
        public RelayCommand LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand ?? (_loginButtonCommand = new RelayCommand( p => _isEnabledLoginButton, LoginAction));
            }
        }

        private Visibility _isVisible = Visibility.Visible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
