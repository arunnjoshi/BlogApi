using MongoDB.Driver;
using System.Collections.Generic;

namespace DataBaseLayer
{
    public interface IMongoCURD
    {
        public List<T> GetRecords<T>();
        public T InsertBlog<T>(T blogPost);
        public DeleteResult DelteBlog<T>(string id);
    }
}
