using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitSync
{
    static class CommandProcessor
    {
        private static string RunCMDCommand(string command, string workingDirectory)
        {
            return RunCommand(command, workingDirectory, OSPlatform.Windows);
        }

        private static string RunBashCommand(string command, string workingDirectory)
        {
            return RunCommand(command, workingDirectory, OSPlatform.Linux);
        }

        private static string RunCommand(string command, string workingDirectory, OSPlatform OS)
        {
            string fileName, arguments;
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
                        WorkingDirectory = workingDirectory
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

        public static bool IsGitDirectory(string directoryPath)
        {            
            return ValidateIsGitRepositoryAnswer(RunCommand(Commands.IsGitRepository, directoryPath, GetCurrentOS()));
        }

        private static bool ValidateIsGitRepositoryAnswer(string outPutResult)
        {
            return outPutResult == "true\n";
        }

        public static OSPlatform GetCurrentOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }
            throw new Exception(Exceptions.OSPlatformNotSupported);
        }
    }
}
