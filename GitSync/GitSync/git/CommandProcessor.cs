using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GitSync.git;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace GitSync
{
    static class CommandProcessor
    {
        public static StageResponse GitAddAll(Repository repository)
        {
            try
            {
                Commands.Stage(repository, "*");
                return new StageResponse(true);
            }
            catch (Exception e)
            {
                return new StageResponse(false, e);
            }
        }

        public static StageResponse GitCommit(Repository repository, GitUser user, string message)
        {
            try
            {
                var author = new Signature(user.UserName, user.Email, DateTime.Now);
                repository.Commit(message, author, author);
                return new StageResponse(true);
            }
            catch (Exception e)
            {
                return new StageResponse(false, e);
            }
        }

        public static StageResponse GitPull(Repository repository, GitUser user)
        {
            try
            {
                Commands.Pull
                (
                    repository,
                    new Signature(new Identity(user.UserName, user.Email), DateTimeOffset.Now),
                    new PullOptions()
                    {
                        FetchOptions = new FetchOptions()
                        {
                            CredentialsProvider = new CredentialsHandler((url, usernameFromUrl, types) =>
                                new UsernamePasswordCredentials()
                                {
                                    Username = user.UserName,
                                    Password = user.Password
                                }
                            )
                        }
                    }
                );
                return new StageResponse(true);
            }
            catch (Exception e)
            {
                return new StageResponse(false, e);
            }
        }

        public static StageResponse GitPush(Repository repository, GitUser user)
        {
            try
            {
                repository.Network.Push
                (
                    repository.Branches["master"],
                    new PushOptions()
                    {
                        CredentialsProvider = new CredentialsHandler((url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = user.UserName,
                                Password = user.Password
                            }
                        )
                    }
                );
                return new StageResponse(true);
            }
            catch (Exception e)
            {
                return new StageResponse(false, e);
            }
        }
    }
}
