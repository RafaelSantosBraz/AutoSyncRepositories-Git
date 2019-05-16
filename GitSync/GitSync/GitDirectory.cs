using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GitSync
{
    class GitDirectory
    {
        private string Path { get; }

        public GitDirectory(string path)
        {
            Path = path;
            Verify();
        }

        private void Verify()
        {
            if (!Directory.Exists(Path))
            {
                throw new Exception(Exceptions.PathIsNotDirectory);
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {

            }
            else
            {
                throw new Exception(Exceptions.OSPlatformNotSupported);
            }
        }
    }
}
