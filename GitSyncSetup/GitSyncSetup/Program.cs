using System;
using System.Collections.Generic;
using System.IO;
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
            long space;
            const string x64Path = @".\x64\bin";
            const string x86Path = @".\x86\bin";
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    space = DirSize(new DirectoryInfo(x64Path));
                }
                else
                {
                    space = DirSize(new DirectoryInfo(x86Path));
                }
            }
            catch
            {
                space = -1;
            }
            if (space == -1)
            {
                Console.WriteLine("It was not possible to read the install files!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Required Space: ~{0}MB", space/1000000); // B to MB
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
                    new Installer(instalationPath, x64Path);
                }
                else
                {
                    new Installer(instalationPath, x86Path);
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
                Console.ReadKey();
            }
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
