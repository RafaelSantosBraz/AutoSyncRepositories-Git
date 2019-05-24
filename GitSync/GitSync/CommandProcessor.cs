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

        public static bool GitCommit(Repository repository, GitUser user, String message)
        {
            try
            {
                Signature author = new Signature(user.UserName, "@" + user.UserName, DateTime.Now);
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
                PullOptions options = new PullOptions()
                {
                    FetchOptions = new FetchOptions()
                    {
                        CredentialsProvider = new CredentialsHandler(
                            (url, usernameFromUrl, types) =>
                                new UsernamePasswordCredentials()
                                {
                                    Username = user.UserName,
                                    Password = user.Password
                                })
                    }
                };
                Signature signature = new Signature(new Identity(user.UserName, "@" + user.UserName), DateTimeOffset.Now);
                Commands.Pull(repository, signature, options);
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
                PushOptions options = new PushOptions()
                {
                    CredentialsProvider = new CredentialsHandler(
                        (url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = user.UserName,
                                Password = user.Password
                            })
                };
                repository.Network.Push(repository.Branches["master"], options);
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
