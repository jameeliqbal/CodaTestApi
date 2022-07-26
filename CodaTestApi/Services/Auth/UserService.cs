using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Services.Auth
{
    public class UserService : IUserService
    {
        private IDictionary<string, (string password, User user)> users =
            new Dictionary<string, (string password, User user)>();

        public UserService(IDictionary<string,string> credentials)
        {
            foreach (var cred in credentials)
            {
                //   this.users.Add(cred.Key.ToLower(), (cred.Value, new User(cred.Key)));
                this.users.Add(cred.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword( cred.Value), new User(cred.Key)));
            }
        }

        public Task<bool> ValidateCredentials(string username, string password, out User user)
        {
            user = null;
            var key = username.ToLower();

            if (users.ContainsKey(key))
            {
                //check password
                //if (password == users[key].password)
                if (BCrypt.Net.BCrypt.Verify(password,users[key].password))
                {
                    user = users[key].user;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
