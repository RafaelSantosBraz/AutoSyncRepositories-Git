using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace GitSync
{
    class GitConfiguration
    {

        public static readonly string StandartConfigurationFilePath = GetStandardPath();

        public GitUser User { get; set; }
        public string ConfigurationFilePath { get; }

        public GitConfiguration()
        {
            ConfigurationFilePath = StandartConfigurationFilePath;
        }

        private static string GetStandardPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return @"C:\GitSync\bin\config.json";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return @"/home/" + Environment.UserName + @"/GitSync/bin/config.json";
            }
            return null;
        }

        public bool VerifyConfigFile()
        {
            if (!File.Exists(ConfigurationFilePath))
            {
                if (CreateConfigurationFile())
                {
                    User = new GitUser();
                    return true;
                }
                else
                {
                    User = null;
                    return false;
                }
            }
            else
            {
                User = ExtractGitUser();
                return true;
            }            
        }

        private GitUser ExtractGitUser()
        {
            try
            {
                return JsonConvert.DeserializeObject<GitUser>(File.ReadAllText(ConfigurationFilePath));
            }
            catch
            {
                return null;
            }
        }

        public static bool CreateConfigurationFile()
        {
            try
            {
                File.WriteAllText(StandartConfigurationFilePath, JsonConvert.SerializeObject(new GitUser(), Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateConfigurationFile()
        {
            try
            {
                File.WriteAllText(StandartConfigurationFilePath, JsonConvert.SerializeObject(User, Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
