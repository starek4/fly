﻿using System.Diagnostics;
using System.Windows;

namespace FlyWindowsWPF.Requests
{
    public static class ApplicationKiller
    {
        private static void KillApp(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Process.GetCurrentProcess().Kill();
        }

        public static void NetworkError()
        {
            KillApp("Error with network connection.");
        }

        public static void DatabaseError()
        {
            KillApp("Error with database connection.");
        }
    }
}
