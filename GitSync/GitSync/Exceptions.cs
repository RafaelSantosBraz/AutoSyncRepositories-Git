using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class Exceptions
    {
        public static readonly string ConfigurationFileManipulationError = "Error during manipulating the configuration file!";
        public static readonly string ConfigurationFileDoesNotExist = "Git configuration file does not Exist!";
        public static readonly string PathIsNotDirectory = "The informed path is not a valid directory!";
        public static readonly string ConfigurationFileCreationError = "Error during creating the configuration file!";
        public static readonly string OSPlatformNotSupported = "The current Operating System is not supported yet!";
        public static readonly string CommandTerminalError = "Error during running internal terminal commands!";
        public static readonly string DirectoryIsNotGit = "The informed path is not a valid Git Repository!";
        public static readonly string ParametersAreNotRight = "Expected parameters: (1) Git directory path - you had better insert it between \"\" - and (2) Commit message between \"\"";
    }
}
