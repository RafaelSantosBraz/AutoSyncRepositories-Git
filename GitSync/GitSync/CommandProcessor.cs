using System;
using System.Diagnostics;

namespace GitSync
{
    static class CommandProcessor
    {
        public static string RunCommand(string command)
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/C " + command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = "."
                }
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
    }
}
