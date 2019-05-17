using System;

namespace GitSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //if (args.Length != 2)
                //{
                //    Console.WriteLine(Exceptions.ParametersAreNotRight);
                //    return;
                //}
                //new GitController(@args[0]).GitAutoSync(args[1]);
                new GitController(@"D:\GitHub\AutoSyncRepositories-Git").GitAutoSync("Adição de command Add e Commit", true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
