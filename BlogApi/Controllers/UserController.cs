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
        private readonly IJwtAuthManager _authManager;
        private readonly IMongoUser _mongoUser;
        private readonly IMapper _mapper;

        public UserController(IJwtAuthManager authManager, IMongoUser mongoUser, IMapper mapper)
        {
            this._authManager = authManager;
            this._mongoUser = mongoUser;
            this._mapper = mapper;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(SignInModel user)
        {
            try
            {
                var token = _authManager.AuthEnticate(user.Email, user.Password);
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
        public IActionResult RegisterUser(UserRegisterationModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var mappedModel = _mapper.Map<UserRegisterationModel, UserModel>(user);
                _mongoUser.RegisterUser(mappedModel);
                return Ok("User Registration Successfully!");
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}