using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitSync
{
    static class CommandProcessor
    {
        //private static string RunCMDCommand(string command, string workingDirectory)
        //{
        //    return RunCommand(command, workingDirectory, OSPlatform.Windows);
        //}

        //private static string RunBashCommand(string command, string workingDirectory)
        //{
        //    return RunCommand(command, workingDirectory, OSPlatform.Linux);
        //}

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

        public static bool GitAdd(string directoryPath)
        {
            return ValidateGitAddAnswer(RunCommand(Commands.GitAdd, directoryPath, GetCurrentOS()));
        }

        private static bool ValidateGitAddAnswer(string outPutResult)
        {
            // It doesn't need any verification yet
            return true;
        }

        public static bool GitCommit(string directoryPath, string commitMessage)
        {
            return ValidateGitCommitAnswer(RunCommand(Commands.GitCommit + FormatCommitMessage(commitMessage), directoryPath, GetCurrentOS()));
        }

        private static bool ValidateGitCommitAnswer(string outPutResult)
        {
            // It doesn't need any verification yet
            return true;
        }

        private static string FormatCommitMessage(string commitMessage)
        {
            if (commitMessage[0] != '\"')
            {
                commitMessage = "\"" + commitMessage;
            }
            if (commitMessage[commitMessage.Length - 1] != '\"')
            {
                commitMessage += "\"";
            }
            return commitMessage;
        }

        public static bool GitPullPrivate(string directoryPath, GitUser user)
        {
            return ValidateGitPullAnswer(RunCommand(Commands.GitPull + "\n" + user.UserName + "\n" + user.Password, directoryPath, GetCurrentOS()));
        }

        public static bool GitPullPublic(string directoryPath)
        {         
            return ValidateGitPullAnswer(RunCommand(Commands.GitPull, directoryPath, GetCurrentOS()));
        }

        private static bool ValidateGitPullAnswer(string outPutResult)
        {
            // It doesn't need any verification yet
            return true;
        }
    }
}
