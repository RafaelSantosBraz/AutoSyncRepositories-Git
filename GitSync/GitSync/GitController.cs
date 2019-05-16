using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitController
    {
        private GitDirectory WorkDirectory { get; }
        private GitConfiguration ConfigurationFile { get; }
        private CommandProcessor Processor { get; }

        public GitController(string workDirectoryPath)
        {
            try
            {
                WorkDirectory = new GitDirectory(workDirectoryPath);
                ConfigurationFile = new GitConfiguration("config.json");
            }
            catch (Exception e)
            {
                if (e.Message == Exceptions.ConfigurationFileDoesNotExist)
                {
                    if (GitConfiguration.CreateConfigurationFile("config.json"))
                    {
                        Console.WriteLine("New configuration file created!");
                    }
                    else
                    {
                        throw new Exception(Exceptions.ConfigurationFileCreationError);
                    }
                }
            }
        }

    }
}
