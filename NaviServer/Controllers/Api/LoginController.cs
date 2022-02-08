using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using NaviServer.Models;
using NaviServer.Code.ControllerLogic;

namespace NaviServer.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            if (LoginLogic.AuthenticateUser(credentials))
            {
                string tokenString = LoginLogic.GenerateJSONWebToken(credentials, _config["Jwt:Issuer"]);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }
}
