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
            EnableLoginButton();
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
                new Thread(StartLoop).Start();
            }
            else
            {
                EnableLoginButton();
            }
        }

        private async void StartLoop()
        {
            HideWindow();
            while (true)
            {
                bool isShutdownPending = await RequestHandler.DoRequest(_client.GetShutdownPending(DeviceIdentifierHelper.DeviceIdentifier, Login));
                if (isShutdownPending)
                {
                    await RequestHandler.DoRequest(_client.ClearShutdownPending(DeviceIdentifierHelper.DeviceIdentifier));
                    TrayController.MakeTooltip("Fly client", "Shutdown request registered.", BalloonIcon.None);
                    ShutdownPc.DoShutdownRequest();
                    return;
                }
                Thread.Sleep(30 * 1000);
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
