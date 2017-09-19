namespace FlyWindowsWPF.PowerShell
{
    public static class DeviceNameHelper
    {
        private static readonly string GetDeviceNameCommand = "$env:computername";
        private static string _deviceName;

        public static string DeviceName => _deviceName ?? (_deviceName = PowerShellWrapper.ExecutePowerShellScript(GetDeviceNameCommand));
    }
}
