using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.TrayIcon
{
    public class TrayController
    {
        private readonly TaskbarIcon _taskbarIconController;

        public TrayController(TaskbarIcon taskbarIconController)
        {
            _taskbarIconController = taskbarIconController;
        }

        public void MakeTooltip(string title, string text, BalloonIcon icon)
        {
            _taskbarIconController.ShowBalloonTip(title, text, icon);
        }
    }
}
