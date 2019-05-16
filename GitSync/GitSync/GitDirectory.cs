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
                throw new Exception(Exceptions.PathIsNotDirectory);
            }
        }

        private bool Verify()
        {
            return Directory.Exists(Path);
        }
    }
}
