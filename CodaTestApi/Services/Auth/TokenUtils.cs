using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodaTestApi.Services.Auth
{
    

    public class TokenUtils
    {
        public static UserAuth BuildUserAuthObject(User user, List<Claim> claims)
        {
            var appUserAuth = new UserAuth();

            // Set User Properties
            appUserAuth.BearerToken = GetToken(user, claims);
            appUserAuth.UserName = user.Username;

            //build user-claims
            foreach (var claim in claims)
            {
                appUserAuth.Claims.Add(new UserClaim { ClaimType = claim.Type, ClaimValue = claim.Value });
            }

            return appUserAuth;
        }


        private static string GetToken(User user, List<Claim> claims)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfigs.Secret));

            var creds = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(TokenConfigs.Issuer, TokenConfigs.Audience, claims, expires: DateTime.UtcNow.AddDays(30), signingCredentials: creds);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
