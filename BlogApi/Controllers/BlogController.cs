using BlogApi.common;
using BlogApi.Models;
using DataBaseLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
    // AuthorizeMultipleRoles custom authorization with roles ('HR,user') ,true means all roles required (default false)
    //// we check all three condition
    [TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "HR,user", true })] // req all roles
    [TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "admin,HR" })]  // require only one roles from HR,admin
                                                                                           ////[TypeFilter(typeof(AuthorizeMultipleRoles), Arguments = new object[] { "PP" })]  // required PP role
                                                                                           //													   // hence user required HR,user,PP or HR,user,admin,PP role to access this controller
    public class BlogController : ControllerBase
	{
		private readonly IMongoCURD db;

		public BlogController(IMongoCURD db)
		{
			this.db   = db;
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
			var blog = db.DeleteBlog<Blog>(id);
			return blog;
		}
	}
}