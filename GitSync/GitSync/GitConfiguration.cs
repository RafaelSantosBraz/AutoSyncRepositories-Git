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
            try
            {
                ConfigurationFilePath = configurationFilePath;
                User = Verify();
            }
            catch (Exception e)
            {
                throw e;
            }
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

        public static void CreateConfigurationFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(new GitUser(), Formatting.Indented));
        }
    }
}
