using System.Diagnostics;
public static class ShellHandler
{
    private static string ExecuteBashCommand(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return result;
    }

    public static string GetDeviceId()
    {
        return GetDeviceName();
    }

    public static string GetDeviceName()
    {
        return ExecuteBashCommand("uname -n");
    }

    public static void Shutdown()
    {
        ExecuteBashCommand("shutdown -h");
    }
}
