using BlogApi.common;
using BlogApi.Models;
using DataBaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // AuthorizeMultipleRoles custom authorization with roles ('HR,user') ,true means all roles required (default false)
    // we check all three condition
    [TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "HR,user",true })] // req all roles 
    [TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "admin,HR" })]  // reqire only one roles from HR,admin
    [TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "PP" })]  // required PP role
    // hence user requied HR,user,PP or HR,user,admin,PP role to acces this controller
    public class BlogController : ControllerBase
    {
        private readonly IMongoCURD db;
        private readonly IOptions<AppSettings> _settings;

        public BlogController(IMongoCURD db, IOptions<AppSettings> settings)
        {
            this.db = db;
            _settings = settings;
        }

        [HttpGet("GetAllBlogs")]
        public IEnumerable<Blog> GetAllBlogs()
        {
            return db.GetRecords<Blog>();
        }

        [HttpGet("InsertBlog")]
        public Blog InsertBlog()
        {
            Blog blog = db.InsertBlog<Blog>(new Blog { Name = "blog test", Description = "api test", User = "postman" });
            return blog;
        }

        [HttpDelete("DeleteBlog")]
        public bool DeleteBlog(string id)
        {
            var blog = db.DelteBlog<Blog>(id);
            return blog;
        }
    }
}