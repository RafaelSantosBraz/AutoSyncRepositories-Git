using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitUser
    {
        private string UserName { get; }
        private string Password { get; }

        public GitUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
