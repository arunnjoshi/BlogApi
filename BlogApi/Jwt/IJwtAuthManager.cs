using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Jwt
{
    public interface IJwtAuthManager
    {
        public string AuthEnticate(string userName, string passWord);
        public TokenValidationParameters GetTokenValidationParameters();
    }
}
