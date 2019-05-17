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
            try
            {
                Path = path;
                Verify();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Verify()
        {
            if (!Directory.Exists(Path))
            {
                throw new Exception(Exceptions.PathIsNotDirectory);
            }
            if (!CommandProcessor.IsGitDirectory(Path))
            {
                throw new Exception(Exceptions.DirectoryIsNotGit);
            }
        }
    }
}
