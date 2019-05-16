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
    }
}
