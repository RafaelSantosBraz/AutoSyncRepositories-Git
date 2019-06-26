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

        public static int GetCaseNumber(Options options)
        {
            if (options.Full)
            {
                return COMPLETE_SYNC;
            }
            return INVALID_CASE;
        }

    }
}
