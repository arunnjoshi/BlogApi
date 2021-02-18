using BlogApi.common;
using BlogApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Jwt
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly string key;
        private readonly IMongoUser mongoUser;

        public JwtAuthManager(AppSettings appSettings, IMongoUser mongoUser)
        {
            key = appSettings.JwtKey;
            this.mongoUser = mongoUser;
        }

        public string AuthEnticate(string userName, string password)
        {
            var user = mongoUser.ValidateUser(userName, password);
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(this.key);
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            return validations;
        }
    }
}