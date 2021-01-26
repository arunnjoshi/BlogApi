using BlogApi.Jwt;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuthManager authManager;

        public UserController(IJwtAuthManager authManager)
        {
            this.authManager = authManager;
        }
        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            var token = authManager.AuthEnticate(user.UserName, user.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
