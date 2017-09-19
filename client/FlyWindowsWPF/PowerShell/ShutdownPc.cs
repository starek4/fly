namespace FlyWindowsWPF.PowerShell
{
    public class ShutdownPc
    {
        private static readonly string ShutdownCommand = "shutdown /s";
        public static void DoShutdownRequest()
        {
            PowerShellWrapper.ExecutePowerShellScript(ShutdownCommand);
        }
    }
}
