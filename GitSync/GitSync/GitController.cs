using System;
using System.Collections.Generic;
using System.Text;
using LibGit2Sharp;

namespace GitSync
{
    class GitController
    {
        public Repository WorkingDirectory { get; }
        public GitConfiguration ConfigurationFile { get; }

        public GitController(string workDirectoryPath)
        {
            try
            {
                WorkingDirectory = new Repository(workDirectoryPath);
                ConfigurationFile = new GitConfiguration("config.json");
            }
            catch (Exception e)
            {
                if (e.Message == Exceptions.ConfigurationFileDoesNotExist)
                {
                    try
                    {
                        GitConfiguration.CreateConfigurationFile("config.json");
                        Console.WriteLine("New configuration file created!");
                    }
                    catch
                    {
                        throw new Exception(Exceptions.ConfigurationFileCreationError);
                    }
                }
                else
                {
                    throw e;
                }
            }
        }

        public bool GitAutoSync(string commitMessage)
        {
            if (!CommandProcessor.GitAddAll(WorkingDirectory))
            {
                return false;
            }
            if (!CommandProcessor.GitCommit(WorkingDirectory, ConfigurationFile.User, commitMessage))
            {
                return false;
            }
            if (!CommandProcessor.GitPull(WorkingDirectory, ConfigurationFile.User))
            {
                return false;
            }
            return CommandProcessor.GitPush(WorkingDirectory, ConfigurationFile.User);
        }

    }
}
