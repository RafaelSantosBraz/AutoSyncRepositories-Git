using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitSync
{
    static class CommandProcessor
    {
        public static string RunCMDCommand(string command)
        {
            return RunCommand(command, OSPlatform.Windows);
        }

        public static string RunBashCommand(string command)
        {
            return RunCommand(command, OSPlatform.Linux);
        }

        private static string RunCommand(string command, OSPlatform OS)
        {
            string fileName = "", arguments = "";
            if (OS == OSPlatform.Linux)
            {
                fileName = "/bin/bash";
                arguments = "-c \" " + command + " \"";
            }
            else if (OS == OSPlatform.Windows)
            {
                fileName = "cmd.exe";
                arguments = "/C " + command;
            }
            else
            {
                throw new Exception(Exceptions.OSPlatformNotSupported);
            }
            try
            {
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = "."
                    }
                };
                process.Start();
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                throw new Exception(Exceptions.CommandTerminalError);
            }
        }
    }
}
