using AutoMapper;
using BlogApi.common;
using BlogApi.Jwt;
using BlogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuthManager authManager;
        private readonly IMongoUser mongoUser;
        private readonly IMapper mapper;

        public UserController(IJwtAuthManager authManager, IMongoUser mongoUser, IMapper mapper)
        {
            this.authManager = authManager;
            this.mongoUser = mongoUser;
            this.mapper = mapper;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            try
            {
                var token = authManager.AuthEnticate(user.UserName, user.Password);
                if (string.IsNullOrEmpty(token))
                    return Unauthorized();
                return Ok(token);
            }
            catch
            {
                return BadRequest("Something Went Wrong");
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserRegisterationModel User)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var mappedModel = mapper.Map<UserRegisterationModel, UserModel>(User);
                mongoUser.RegisterUser(mappedModel);
                return Ok("User Registeration Successfully!");
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}