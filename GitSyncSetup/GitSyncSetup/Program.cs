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
            const string instalationPath = @"C:\GitSync\bin";
            Console.WriteLine("GitSync Installer");
            Console.WriteLine("");
            Console.WriteLine("Instalation Path: '" + instalationPath + "'");
            int space;            
            if (Environment.Is64BitOperatingSystem)
            {
                space = 76;
            }
            else
            {
                space = 66;
            }
            Console.WriteLine("Required Space: ~{0}MB", space);
            Console.WriteLine("");
            Console.WriteLine("Do you want to continue? (Y/N)");
            var resp = Console.ReadKey();
            if (char.ToUpper(resp.KeyChar) != 'Y')
            {
                return;
            }
            try
            {                
                if (Environment.Is64BitOperatingSystem)
                {
                    new Installer(instalationPath, @".\x64\bin");
                }
                else
                {
                    new Installer(instalationPath, @".\x86\bin");
                }
                Console.WriteLine("");
                Console.WriteLine("GitSync was completely installed!");
                Console.WriteLine("Press any key to close...");
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
