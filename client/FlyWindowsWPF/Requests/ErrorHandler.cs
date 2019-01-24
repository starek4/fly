using System.Diagnostics;
using System.Windows;
using FlyWindowsWPF.TrayIcon;
using Hardcodet.Wpf.TaskbarNotification;

namespace FlyWindowsWPF.Requests
{
    public static class ErrorHandler
    {
        public static bool IsNetworkError;
        private static void KillApp(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Process.GetCurrentProcess().Kill();
        }

        public static void NetworkError(TrayController controller)
        {
            controller.MakeIconGray();
        }

        public static void DatabaseError()
        {
            KillApp("Error with database connection or query.");
        }

        public static void DeletedDevice()
        {
            if (!IsNetworkError)
                KillApp("Device was deleted. You must login again into fly application.");
        }
    }
}
