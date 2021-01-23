using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogApi.Models
{
	public class Blog
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id   { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
        public string User { get; set; }
    }
}