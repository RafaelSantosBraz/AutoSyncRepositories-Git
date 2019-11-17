using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public GitUser(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        public GitUser()
        {
            UserName = "";
            Password = "";
            Email = "";
        }
    }
}
