using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace FlyWindows
{
    public static class PowerShellHandler
    {
        private static readonly string GetUUIDString = "get-wmiobject Win32_ComputerSystemProduct  | Select-Object -ExpandProperty UUID";
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
            return ExecutePowerShellScript(GetUUIDString);
        }

        public static void ShutdownPc()
        {
            ExecutePowerShellScript("shutdown /s");
        }

        public static string GetDeviceName()
        {
            return ExecutePowerShellScript("$env:computername");
        }
    }
}
