using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class Commands
    {
        public static readonly string IsGitRepository = " git rev-parse --is-inside-work-tree";
    }
}
