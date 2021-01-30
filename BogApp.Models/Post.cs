using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BogApp.Models
{
    public class Post : Entity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string? Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}
