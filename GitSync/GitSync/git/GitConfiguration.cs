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

        public bool CheckConfigFileExists()
        {
            return File.Exists(ConfigurationFilePath);
        }

        public bool VerifyConfigFile()
        {
            if (!CheckConfigFileExists())
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
                string dFile = EncryptMessages.Decrypt(File.ReadAllText(ConfigurationFilePath));
                if (dFile == null)
                {
                    return null;
                }
                GitUser tUser = JsonConvert.DeserializeObject<GitUser>(dFile);
                if (tUser == null)
                {
                    return null;
                }
                return new GitUser()
                {
                    UserName = EncryptMessages.Decrypt(tUser.UserName),
                    Email = EncryptMessages.Decrypt(tUser.Email),
                    Password = EncryptMessages.Decrypt(tUser.Password)
                };
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
                string s = EncryptMessages.Encrypt(" ");
                File.WriteAllText
                (
                    StandartConfigurationFilePath,
                    EncryptMessages.Encrypt(JsonConvert.SerializeObject(new GitUser(s, s, s), Formatting.Indented))
                );                
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
                GitUser tUser = new GitUser() {
                    UserName = EncryptMessages.Encrypt(User.UserName),
                    Password = EncryptMessages.Encrypt(User.Password),
                    Email = EncryptMessages.Encrypt(User.Email)
                };
                File.WriteAllText
                (
                    StandartConfigurationFilePath,
                    EncryptMessages.Encrypt(JsonConvert.SerializeObject(tUser, Formatting.Indented))
                );                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RequireUserChange()
        {
            if (User == null)
            {
                Console.WriteLine(Exceptions.ConfigureGitUser);
            }
            else
            {
                Console.WriteLine(Exceptions.ChangeGitUser);
            }
            try
            {
                Console.Write("* Git username: ");
                string username = Console.ReadLine();
                Console.Write("* Git email: ");
                string email = Console.ReadLine();
                Console.Write("* Git password: ");
                string pass = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, pass.Length - 1);
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                User = new GitUser(username, pass, email);
                return ApplyUserChanges();
            }
            catch
            {
                return false;
            }
        }

        private bool ApplyUserChanges()
        {
            return UpdateConfigurationFile();
        }
    }
}
