using CodaTestApi.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodaTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("signIn")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                User user;

                if (await userService.ValidateCredentials(model.UserName, model.Password, out user))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,model.UserName),
                    new Claim("name",model.UserName)
                };

                    var authUser = TokenUtils.BuildUserAuthObject(user, claims);

                    return Ok(authUser);
                }
            }
            return BadRequest();
        }
    }
}
