using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.TrayIcon
{
    public class TrayController
    {
        private readonly TaskbarIcon taskbarIconController;

        public TrayController(TaskbarIcon taskbarIconController)
        {
            this.taskbarIconController = taskbarIconController;
        }

        public void MakeTooltip(string title, string text, BalloonIcon icon)
        {
            taskbarIconController.ShowBalloonTip(title, text, icon);
        }
    }
}
