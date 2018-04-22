using System.Diagnostics;
using System.Windows;
using FlyWindowsWPF.TrayIcon;
using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.Requests
{
    public static class ErrorHandler
    {
        private static void KillApp(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Process.GetCurrentProcess().Kill();
        }

        public static void NetworkError(TrayController controller)
        {
            controller.MakeIconGray();
            controller.MakeTooltip("Fly client", "Something wrong with internet connection...", BalloonIcon.None);
        }

        public static void DatabaseError()
        {
            KillApp("Error with database connection or query.");
        }

        public static void DeletedDevice()
        {
            KillApp("Device was deleted. You must login again into fly application.");
        }
    }
}
