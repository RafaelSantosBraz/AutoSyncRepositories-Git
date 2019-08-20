using GitSync.arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class Arguments
    {
        // error during processing methods
        public const int ERROR_CONFIGURATION_FILE = -2;
        // options are not right together
        public const int INVALID_CASE = -1;
        // do a complete sync (add, commit, pull, push)
        public const int COMPLETE_SYNC = 0;

        public static int GetCaseNumber(Options options)
        {
            if (options.Path == null)
            {
                options.Path = CurrentWorkingDirectory();                
            }
            if (options.Commit == null)
            {
                options.Commit = CurrentDateTime();
            }
            try
            {
                AplyUserChanges(options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ERROR_CONFIGURATION_FILE;
            }
            if (options.Full)
            {
                return COMPLETE_SYNC;
            }
            return INVALID_CASE;
        }


        public static string CurrentWorkingDirectory()
        {
            try
            {
                return Environment.CurrentDirectory;
            }
            catch
            {
                Console.WriteLine(Exceptions.InvalidWorkingDirectory);
                return Exceptions.InvalidWorkingDirectory;
            }
        }

        public static string CurrentDateTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        public static void AplyUserChanges(Options options)
        {
            if (options.Username != null || options.Password != null || options.Email != null)
            {
                try
                {
                    var configuration = new GitConfiguration();
                    if (options.Username != null)
                    {
                        configuration.User.UserName = options.Username;
                    }
                    if (options.Password != null)
                    {
                        configuration.User.Password = options.Password;
                    }
                    if (options.Email != null)
                    {
                        configuration.User.Email = options.Email;
                    }
                    configuration.UpdateConfigurationFile();
                    Console.WriteLine(Exceptions.GitUserUpdated);
                }
                catch (Exception e)
                {
                    if (e.Message == Exceptions.ConfigurationFileDoesNotExist)
                    {
                        try
                        {
                            GitConfiguration.CreateConfigurationFile();
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
        }
    }
}
