using GitSync.arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class Arguments
    {        
        // options are not right together
        public const int INVALID_CASE = -1;
        // do a complete sync (add, commit, pull, push)
        public const int COMPLETE_SYNC = 0;
        // request user changes
        public const int USER_CHANGES = 1;

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
            if (options.ChangeUser)
            {
                return USER_CHANGES;
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
                return null;
            }
        }

        public static string CurrentDateTime()
        {
            try
            {
                return DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return null;
            }
        }

    }
}
