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
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
