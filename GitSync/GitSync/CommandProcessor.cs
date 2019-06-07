using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace GitSync
{
    static class CommandProcessor
    {
        public static bool GitAddAll(Repository repository)
        {
            try
            {
                Commands.Stage(repository, "*");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool GitCommit(Repository repository, GitUser user, string message)
        {
            try
            {
                var author = new Signature(user.UserName, user.Email, DateTime.Now);
                repository.Commit(message, author, author);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool GitPull(Repository repository, GitUser user)
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool GitPush(Repository repository, GitUser user)
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
