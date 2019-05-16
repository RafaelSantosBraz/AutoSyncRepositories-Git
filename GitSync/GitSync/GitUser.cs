using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public GitUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public GitUser()
        {
            UserName = "";
            Password = "";
        }
    }
}
