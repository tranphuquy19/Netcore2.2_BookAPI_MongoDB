using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Doraneko.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }
        public string Name { get; set; }
    }
}