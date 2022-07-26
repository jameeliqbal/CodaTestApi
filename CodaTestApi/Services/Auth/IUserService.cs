using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Services.Auth
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string username, string password, out User user);
    }
}
