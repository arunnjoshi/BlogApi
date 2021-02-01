using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer
{
	public interface IMongoCURD
	{
		public List<T> GetRecords<T>();
		public T InsertBlog<T>(T blogPost);
		public bool DeleteBlog<T>(string id);
	}
}
