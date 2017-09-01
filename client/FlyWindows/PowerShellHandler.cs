using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace FlyWindows
{
    public static class PowerShellHandler
    {
        private static readonly string GetUUIDStringCommand = "get-wmiobject Win32_ComputerSystemProduct  | Select-Object -ExpandProperty UUID";
        private static readonly string ShutdownCommand = "shutdown /s";
        private static readonly string GetDeviceNameCommand = "$env:computername";

        private static string ExecutePowerShellScript(string script)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(script);
            pipeline.Commands.Add("Out-String");
            var results = pipeline.Invoke();
            runspace.Close();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }
            return stringBuilder.ToString();
        }

        public static string GetUUID()
        {
            return ExecutePowerShellScript(GetUUIDStringCommand);
        }

        public static void ShutdownPc()
        {
            ExecutePowerShellScript(ShutdownCommand);
        }

        public static string GetDeviceName()
        {
            return ExecutePowerShellScript(GetDeviceNameCommand);
        }
    }
}
