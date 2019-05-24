using System;
using LibGit2Sharp;

namespace GitSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine(Exceptions.ParametersAreNotRight);
                    return;
                }
                new GitController(@args[0]).GitAutoSync(args[1]);
                Console.WriteLine("Synced!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
