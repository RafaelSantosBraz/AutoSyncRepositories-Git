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
        public static readonly string NewConfigurationFileCreated = "New configuration file created! Please fill it with your user information.";
        public static readonly string ImcompletedSync = "Could not completely sync!";
        public static readonly string InvalidArgsCombination = "The informed arguments are not valid!";
        public static readonly string InvalidWorkingDirectory = "Invalid Working Directory path!";
        public static readonly string GitUserUpdated = "Git user changes successfully applied!";
        public static readonly string ErrorManipulatingConfigFile = "Error during processing configuration file!";
        public static readonly string AddingFiles = "Adding files...";
        public static readonly string CommittingChanges = "Committing changes...";
        public static readonly string PullingRepository = "Pulling repository...";
        public static readonly string PushingRepository = "Pushing repository...";
        public static readonly string ProgressFinished = "Finished!";
        public static readonly string ConfigureGitUser = "Insert valid GitHub credentials before using GitSync please:\n";
        public static readonly string ChangeGitUser = "Insert valid GitHub credentials to change your stored Git user:\n";
        public static readonly string UserChangesApplied = "Git user successfully changed!";
        public static readonly string UserChangesNotApplied = "It was not possible to change Git user! Try again later please.";
        public static readonly string UserCreated = "Git user successfully created!";
        public static readonly string UserNotCreated = "It was not possible to create Git user! Try again later please.";
    }
}
