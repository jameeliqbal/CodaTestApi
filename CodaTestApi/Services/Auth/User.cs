using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Services.Auth
{
    public class User
    {
        public string Username { get; }

        public User(string username)
        {
            this.Username = username;
        }
    }
}
