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
            //CommandLine.Parser.Default.ParseArguments<Options>(args)
            //    .WithParsed(opts => RunOptionsAndReturnExitCode(opts));

            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine(Exceptions.ParametersAreNotRight);
                    return;
                }
                if (new GitController(args[0]).GitAutoSync(args[1]))
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

        static int RunOptionsAndReturnExitCode(Options options)
        {
            
            return 0;
        }
    }
}
