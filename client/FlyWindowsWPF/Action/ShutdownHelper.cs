using FlyWindowsWPF.PowerShell;

namespace FlyWindowsWPF.Action
{
    public static class ShutdownHelper
    {
        private static readonly string ShutdownCommand = "shutdown /s";
        private static readonly string RestartCommand = "shutdown /r";
        private static readonly string SleepCommand = "shutdown /h";
        public static void DoShutdownRequest()
        {
            PowerShellWrapper.ExecutePowerShellScript(ShutdownCommand);
        }

        public static void DoRestartRequest()
        {
            PowerShellWrapper.ExecutePowerShellScript(RestartCommand);
        }

        public static void DoSleepRequest()
        {
            PowerShellWrapper.ExecutePowerShellScript(SleepCommand);
        }
    }
}
