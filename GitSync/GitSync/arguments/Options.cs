using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace GitSync.arguments
{
    class Options
    {      
        [Option('p', "path", Required = true, HelpText = "Git Repository Path.")]
        public string Path { get; set; }

        [Option('c', "commit", Required = false, HelpText = "Git Commit Message.")]
        public string Commit { get; set; }
    }
}
