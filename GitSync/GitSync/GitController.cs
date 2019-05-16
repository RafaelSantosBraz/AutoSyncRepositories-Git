using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitController
    {
        public GitDirectory WorkDirectory { get; }
        public GitConfiguration ConfigurationFile { get; }
        public CommandProcessor Processor { get; }

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

    }
}
