using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GitSync
{
    class GitConfiguration
    {
        private GitUser User { get; }
        private string ConfigurationFilePath { get; }
        public GitConfiguration(string configurationFilePath)
        {
            ConfigurationFilePath = configurationFilePath;
            User = Verify();
        }

        private GitUser Verify()
        {
            if (!File.Exists(ConfigurationFilePath))
            {
                throw new Exception(Exceptions.ConfigurationFileDoesNotExist);
            }
            try
            {
                return JsonConvert.DeserializeObject<GitUser>(File.ReadAllText(ConfigurationFilePath));
            }
            catch 
            {
                throw new Exception(Exceptions.ConfigurationFileManipulationError);
            }
        }
    }
}
