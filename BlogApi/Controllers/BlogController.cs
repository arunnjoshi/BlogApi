using BlogApi.Models;
using DataBaseLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly MongoCURD db;

        public BlogController()
        {
            db = new MongoCURD("ArunJoshi");
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