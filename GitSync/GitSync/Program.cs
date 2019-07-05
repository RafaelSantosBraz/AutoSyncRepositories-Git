using System;
using LibGit2Sharp;
using CommandLine;
using GitSync.arguments;

namespace GitSync
{
    class Program
    {

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(opts => ExecuteWithOptions(opts));            
        }

        static void ExecuteWithOptions(Options options)
        {
            int optionsCase = Arguments.GetCaseNumber(options);
            if (optionsCase == Arguments.INVALID_CASE)
            {
                Console.WriteLine(Exceptions.InvalidArgsCombination);
                return;
            }
            else if (optionsCase == Arguments.ERROR_CONFIGURATION_FILE)
            {
                Console.WriteLine(Exceptions.ErrorManipulatingConfigFile);
                return;
            }
            try
            {
                if (new GitController(options.Path).ExecuteCase(optionsCase, options))
                {
                    Console.WriteLine("\nSynced!");
                }
                else
                {
                    Console.WriteLine(Exceptions.ImcompletedSync);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
