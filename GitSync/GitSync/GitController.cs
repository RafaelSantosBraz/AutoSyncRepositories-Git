using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitController
    {
        public GitDirectory WorkingDirectory { get; }
        public GitConfiguration ConfigurationFile { get; }

        public GitController(string workDirectoryPath)
        {
            try
            {
                WorkingDirectory = new GitDirectory(workDirectoryPath);
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

        public bool GitAutoSync(string commitMessage, bool isPrivate)
        {
            try
            {
                if (!CommandProcessor.GitAdd(WorkingDirectory.Path))
                {
                    return false;
                }
                if (!CommandProcessor.GitCommit(WorkingDirectory.Path, commitMessage))
                {
                    return false;
                }
                if (isPrivate)
                {
                    if (!CommandProcessor.GitPullPrivate(WorkingDirectory.Path, ConfigurationFile.User))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!CommandProcessor.GitPullPublic(WorkingDirectory.Path))
                    {
                        return false;
                    }
                }
                return CommandProcessor.GitPush(WorkingDirectory.Path, ConfigurationFile.User);                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
