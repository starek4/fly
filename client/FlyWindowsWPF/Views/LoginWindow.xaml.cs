using System;
using System.ComponentModel;
using System.Windows;
using FlyWindowsWPF.TrayIcon;
using FlyWindowsWPF.ViewModels;

namespace FlyWindowsWPF.Views
{
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel { TrayController = new TrayController(ShutdownNotifyIcon) };
        }

        protected override void OnStateChanged(EventArgs eventArgs)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(eventArgs);
        }

        protected override void OnClosing(CancelEventArgs eventArgs)
        {
            eventArgs.Cancel = true;
            Hide();
            base.OnClosing(eventArgs);
        }
    }
}
