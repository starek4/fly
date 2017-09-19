using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace FlyWindowsWPF.PowerShell
{
    public static class PowerShellWrapper
    {
        public static string ExecutePowerShellScript(string script)
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
            return stringBuilder.ToString().Replace(Environment.NewLine, String.Empty);
        }
    }
}
