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
        private string password;
        private readonly ILogger logger = EnviromentHelper.GetLogger();
        private readonly Client client = new Client();
        private string status;
        private bool isEnabledLoginButton;
        public TrayController TrayController { private get; set; }

        public string Login { get; set; }

        public LoginViewModel()
        {
            EnableLoginButton();
        }

        public string Status
        {
            get => status;
            set
            {
                status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }

        private void LoginAction(object parameter)
        {
            DisableLoginButton();
            if (parameter is PasswordBox passwordContainer)
            {
                password = passwordContainer.Password;
            }
            else
            {
                logger.Fatal("Wrong command argument provided.");
                throw new NotImplementedException("Wrong command argument.");
            }

            new Thread(async () => await ProcessLogin()).Start();
        }
        
        private async Task ProcessLogin()
        {
            bool isUserVerified = await RequestHandler.DoRequest(client.VerifyUserLogin(Login, password));
            Status = isUserVerified ? "Login successfully verified." : "Login denied!";
            if (isUserVerified)
            {
                bool isDeviceRegistered = await RequestHandler.DoRequest(client.VerifyDeviceId(DeviceIdentifierHelper.DeviceIdentifier));
                if (isDeviceRegistered == false)
                    await RequestHandler.DoRequest(client.AddDevice(Login, DeviceIdentifierHelper.DeviceIdentifier, DeviceNameHelper.DeviceName));
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
                bool isShutdownPending = await RequestHandler.DoRequest(client.GetShutdownPending(DeviceIdentifierHelper.DeviceIdentifier, Login));
                if (isShutdownPending)
                {
                    await RequestHandler.DoRequest(client.ClearShutdownPending(DeviceIdentifierHelper.DeviceIdentifier));
                    TrayController.MakeTooltip("Fly client", "Shutdown request registered.", BalloonIcon.None);
                    ShutdownPc.DoShutdownRequest();
                    return;
                }
                Thread.Sleep(30 * 1000);
            }

        }

        private void EnableLoginButton()
        {
            isEnabledLoginButton = true;
            LoginButtonCommand.Refresh();
        }

        private void DisableLoginButton()
        {
            isEnabledLoginButton = false;
            LoginButtonCommand.Refresh();
        }

        private void HideWindow()
        {
            IsVisible = Visibility.Hidden;
            TrayController.MakeTooltip("Fly client", "Running in background...", BalloonIcon.None);
        }

        private CommandHandler loginButtonCommand;

        public CommandHandler LoginButtonCommand => loginButtonCommand ?? (loginButtonCommand = new CommandHandler(LoginAction, something => isEnabledLoginButton));

        private Visibility isVisible = Visibility.Visible;
        public Visibility IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
