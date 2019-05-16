using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GitSync
{
    class GitConfiguration
    {
        public GitUser User { get; }
        public string ConfigurationFilePath { get; }

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

        public static bool CreateConfigurationFile(string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(new GitUser(), Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
