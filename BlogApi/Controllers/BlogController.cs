using BlogApi.Models;
using DataBaseLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost("InsertBlog")]
        public IActionResult InsertBlog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(blog);
            }
            Blog res = db.InsertBlog(blog);
            return Ok(blog);
        }

        [HttpDelete("DeleteBlog")]
        public string DeleteBlog(string id)
        {
            var blog = db.DelteBlog<Blog>(id);
            return "done";
        }
    }
}