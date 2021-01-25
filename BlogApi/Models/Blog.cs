using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [MinLength(10)]
        public string User { get; set; }
    }
}