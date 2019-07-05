using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitSync;
using ShellProgressBar;

namespace GitSyncSetup
{
    class Installer
    {
        public string InstallPath { get; }
        public string BinPath { get; }

        private ProgressBar Bar { get; set; }

        public Installer(string installPath, string binPath)
        {
            InstallPath = installPath;
            BinPath = binPath;
            try
            {
                using (Bar = ProgressView.CreateSimpleProgressBar(5, "Installing GitSync..."))
                {
                    Prepare();
                    Install();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Install()
        {
            try
            {
                Bar.Tick("Extracting files...");
                DirectoryCopy(BinPath, InstallPath);
                Bar.Tick("Configurating System...");
                CreateEnvironmentVariable();
                Bar.Tick("Finished!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Prepare()
        {
            try
            {
                Bar.Tick("Veryfing 'bin' directory...");
                if (!Directory.Exists(BinPath))
                {
                    throw new Exception("Bin directory not found! Try download again please.");
                }
                Bar.Tick("Veryfing instalation directory...");
                if (Directory.Exists(InstallPath))
                {
                    Directory.Delete(InstallPath, true);
                }
            }
            catch
            {
                throw new Exception("Error during manipulating install directory!");
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);
                DirectoryInfo[] dirs = dir.GetDirectories();
                Directory.CreateDirectory(destDirName);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void CreateEnvironmentVariable()
        {
            try
            {
                const string name = "PATH";                
                string pathvar = Environment.GetEnvironmentVariable(name);
                var value = pathvar + ";" + InstallPath;
                var target = EnvironmentVariableTarget.User;
                Environment.SetEnvironmentVariable(name, value, target);
            }
            catch
            {
                throw new Exception("Error during creating Environment variable!");
            }
        }
    }
}
