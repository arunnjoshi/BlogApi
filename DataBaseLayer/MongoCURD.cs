
using DataBaseLayer.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DataBaseLayer
{
    public class MongoCURD : IMongoCURD
    {
        private readonly IMongoDatabase db;
        private readonly string collectionName = "Blog";
        public MongoCURD(DbAppSettings appSettings)
        {
            MongoClient client = new MongoClient(appSettings.ConnectionString);
            db = client.GetDatabase(appSettings.DbName);
        }

        public List<T> GetRecords<T>()
        {
            IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
            List<T> list = collection.Find(new BsonDocument()).ToList();
            return list;
        }

        public T InsertBlog<T>(T blogPost)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
            collection.InsertOne(blogPost);
            return blogPost;
        }

        public bool DeleteBlog<T>(string id)
        {
            try
            {
                IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
                var deletedBlog = collection.DeleteOne(Builders<T>.Filter.Eq("Id", id));
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}