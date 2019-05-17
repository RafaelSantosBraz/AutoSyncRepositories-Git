using System;

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
                GitController controller = new GitController(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
