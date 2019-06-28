using System;
using System.Collections.Generic;
using System.Text;
using GitSync.arguments;
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
                    }
                    catch
                    {                      
                        throw new Exception(Exceptions.ConfigurationFileCreationError);
                    }
                    throw new Exception(Exceptions.NewConfigurationFileCreated);
                }
                else
                {
                    throw e;
                }
            }
        }

        private bool GitAutoSync(string commitMessage)
        {
            using (var bar = ProgressView.CreateSimpleProgressBar(4, "Complete Git Sync"))
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

        public bool ExecuteCase(int optionsCase, Options options)
        {
            switch (optionsCase)
            {
                case Arguments.COMPLETE_SYNC:
                    return GitAutoSync(options.Commit);
            }
            return true;
        }
    }
}
