using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project
{
    public abstract class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
