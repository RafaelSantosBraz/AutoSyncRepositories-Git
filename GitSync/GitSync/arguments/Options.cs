using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace GitSync.arguments
{
    class Options
    {      
        [Option('p', "path", Required = false, HelpText = "Git Repository Path.")]
        public string Path { get; set; }

        [Option('c', "commit", Required = false, HelpText = "Git Commit Message.")]
        public string Commit { get; set; }

        [Option('f', "full", Required = false, Default = true, HelpText = "Complete Git Sync (add, commit, pull, and push)")]
        public bool Full { get; set; }
    }
}
