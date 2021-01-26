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
    [Authorize(Roles ="user")]

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
        public string DeleteBlog(string id)
        {
            var blog = db.DelteBlog<Blog>(id);
            return "done";
        }
    }
}