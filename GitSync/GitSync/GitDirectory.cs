using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitSync
{
    class GitDirectory
    {
        private string Path { get; }

        public GitDirectory(string path)
        {
            Path = path;
            if (!Verify())
            {
                throw new Exception("The informed path is not a valid directory!");
            }
        }

        private bool Verify()
        {
            return Directory.Exists(Path);
        }
    }
}
