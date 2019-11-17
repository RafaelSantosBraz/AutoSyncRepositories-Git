using System;
using LibGit2Sharp;
using CommandLine;
using GitSync.arguments;

namespace GitSync
{
    // start point class
    class Program
    {

        // parses the command line arguments and executes them
        static void Main(string[] args) => Parser.Default.ParseArguments<Options>(args).WithParsed(opts => ExecuteWithOptions(opts));

        static void ExecuteWithOptions(Options options)
        {

            var config = new GitConfiguration();
            if (!config.CheckConfigFileExists())
            {
                if (!config.RequireUserChange())
                {
                    Console.WriteLine(Exceptions.UserCreated);
                    return;
                }
                Console.WriteLine(Exceptions.UserNotCreated);
                return;
            }
            int optionsCase = Arguments.GetCaseNumber(options);
            switch (optionsCase)
            {
                case Arguments.INVALID_CASE:
                    Console.WriteLine(Exceptions.InvalidArgsCombination);
                    break;
                case Arguments.USER_CHANGES:
                    if (config.RequireUserChange())
                    {
                        Console.WriteLine(Exceptions.UserChangesApplied);
                        return;
                    }
                    Console.WriteLine(Exceptions.UserChangesApplied);
                    break;
                default:
                    if (new GitController(options.Path).ExecuteCase(optionsCase, options))
                    {
                        Console.WriteLine("\n * Synced! * ");
                    }
                    else
                    {
                        Console.WriteLine(Exceptions.ImcompletedSync);
                    }
                    break;
            }
        }
    }
}
