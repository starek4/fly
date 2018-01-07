using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.TrayIcon
{
    public class TrayController
    {
        private readonly TaskbarIcon _taskbarIconController;
        private bool _isRed = true;

        public TrayController(TaskbarIcon taskbarIconController)
        {
            _taskbarIconController = taskbarIconController;
        }

        public void MakeTooltip(string title, string text, BalloonIcon icon)
        {
            _taskbarIconController.ShowBalloonTip(title, text, icon);
        }

        public void MakeIconGray()
        {
            if (_isRed)
                Application.Current.Dispatcher.Invoke(() => { _taskbarIconController.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/FlyClient;component/shutdown_gray.ico")); });
            _isRed = false;
        }

        public void MakeIconRed()
        {
            if (!_isRed)
                Application.Current.Dispatcher.Invoke(() => { _taskbarIconController.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/FlyClient;component/shutdown.ico")); });
            _isRed = true;
        }
    }
}
