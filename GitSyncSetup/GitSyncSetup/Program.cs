using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSyncSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string instalationPath = @"C:\Program Files\GitSync\bin";
                if (Environment.Is64BitOperatingSystem)
                {
                    new Installer(instalationPath, @".\x64\bin");
                }
                else
                {
                    new Installer(instalationPath, @".\x86\bin");
                }
                Console.WriteLine("GitSync was completely installed!");
                Console.WriteLine("Press any key to close this close...");
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Could not complete installing! Try again later please.");
            }
        }
    }
}
