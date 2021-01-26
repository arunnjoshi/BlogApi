using BlogApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Jwt
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly string key;

        public JwtAuthManager(string key)
        {
            this.key = key;
        }
        private List<User> users = new List<User>
        {
            new User{UserName="arun",Password="test@123",Roles="user"}

        };

        public string AuthEnticate(string userName, string password)
        {
            var user = users.Where(x => x.UserName == userName && x.Password == password).First();
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName),
                    new Claim(ClaimTypes.Role,user.Roles)

                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }
    }
}
