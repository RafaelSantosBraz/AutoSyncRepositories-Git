using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace GitSync.arguments
{
    // argument line options
    class Options
    {      
        [Option('p', "path", Required = false, HelpText = "Git Repository Path.")]
        public string Path { get; set; }

        [Option('c', "commit", Required = false, HelpText = "Git Commit Message.")]
        public string Commit { get; set; }

        [Option('f', "full", Required = false, Default = true, HelpText = "Complete Git Sync (add all, commit, pull, and push).")]
        public bool Full { get; set; }

        [Option("change-user", Required = false, Default = false, HelpText = "Change the storaged Git user.")]
        public bool ChangeUser { get; set; }       
    }
}
