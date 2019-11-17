using System;
using System.Collections.Generic;
using System.Text;
using GitSync.arguments;
using LibGit2Sharp;

namespace GitSync
{
    class GitController
    {
        public Repository WorkingDirectory { get; }
        public GitConfiguration ConfigurationFile { get; }

        public GitController(string workDirectoryPath)
        {
            WorkingDirectory = new Repository(workDirectoryPath);
            ConfigurationFile = new GitConfiguration();                        
        }

        public bool Verify()
        {
            return ConfigurationFile.VerifyConfigFile();
        }

        private bool GitAutoSync(string commitMessage)
        {
            using (var bar = ProgressView.CreateSimpleProgressBar(4, Exceptions.AddingFiles, true))
            {
                var resp = CommandProcessor.GitAddAll(WorkingDirectory);
                if (!resp.Response)
                {
                    bar.Dispose();
                    Console.WriteLine(resp.Ex.Message);
                    return false;
                }
                bar.Tick(Exceptions.CommittingChanges);
                var commit = CommandProcessor.GitCommit(WorkingDirectory, ConfigurationFile.User, commitMessage);
                if (!commit.Response)
                {
                    if (!(commit.Ex is EmptyCommitException))
                    {
                        bar.Dispose();
                        Console.WriteLine(commit.Ex.Message);
                        return false;
                    }
                }
                bar.Tick(Exceptions.PullingRepository);
                resp = CommandProcessor.GitPull(WorkingDirectory, ConfigurationFile.User);
                if (!resp.Response)
                {
                    bar.Dispose();
                    Console.WriteLine(resp.Ex.Message);
                    return false;
                }
                bar.Tick(Exceptions.PushingRepository);
                if (commit.Response)
                {
                    resp = CommandProcessor.GitPush(WorkingDirectory, ConfigurationFile.User);
                    if (resp.Response)
                    {
                        bar.Tick(Exceptions.ProgressFinished);
                        return true;
                    }
                    bar.Dispose();
                    Console.WriteLine(resp.Ex.Message);
                    return false;
                }
                bar.Tick(Exceptions.ProgressFinished);
                return true;
            }
        }

        public bool ExecuteCase(int optionsCase, Options options)
        {
            switch (optionsCase)
            {
                case Arguments.COMPLETE_SYNC:
                    return GitAutoSync(options.Commit);
            }
            return true;
        }
    }
}
