namespace FlyWindowsWPF.PowerShell
{
    public static class DeviceIdentifierHelper
    {
        private static readonly string GetUUIDStringCommand = "get-wmiobject Win32_ComputerSystemProduct  | Select-Object -ExpandProperty UUID";
        private static string _deviceIdentifier;

        public static string DeviceIdentifier => _deviceIdentifier ?? (_deviceIdentifier = PowerShellWrapper.ExecutePowerShellScript(GetUUIDStringCommand));
    }
}
