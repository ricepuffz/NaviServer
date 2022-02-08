using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using NaviServer.Data;
using NaviServer.Models;

namespace NaviServer.Code.ControllerLogic
{
    public static class LoginLogic
    {
        public static string GenerateJSONWebToken(Credentials credentials, string jwtIssuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.JWTSecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("sub", credentials.Username),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            using (var db = new NaviContext())
            {
                Credentials retrievedCredentials = db.Credentials.Where(e => e.Username == credentials.Username).FirstOrDefault();
                if (retrievedCredentials != null && retrievedCredentials.Player.IsAdmin == 1)
                    claims.Add(new Claim("role", "Administrator"));
            }

            claims.Add(new Claim("role", "User"));

            var token = new JwtSecurityToken(jwtIssuer,
                jwtIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool AuthenticateUser(Credentials credentials)
        {
            Credentials retrievedCredentials;

            using (var db = new NaviContext())
                retrievedCredentials = db.Credentials.Where(e => e.Username == credentials.Username).FirstOrDefault();

            string hashedPassword = Util.HashSHA256(credentials.Password);

            if (retrievedCredentials == null)
                return false;

            return hashedPassword == retrievedCredentials.Password;
        }
    }
}
