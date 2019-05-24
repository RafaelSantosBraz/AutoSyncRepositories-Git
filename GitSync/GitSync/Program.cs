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
                new GitController(@"D:\GitHub\Computacao-Grafica").GitAutoSync("Configuração de projeto", false);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
