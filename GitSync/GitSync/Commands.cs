using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class Commands
    {
        public static readonly string IsGitRepository = " git rev-parse --is-inside-work-tree";
        public static readonly string GitAdd = " git add .";
        public static readonly string GitPull = " git pull";
        public static readonly string GitPush = " git push";
        public static readonly string GitCommit = " git commit -m ";
    }
}
