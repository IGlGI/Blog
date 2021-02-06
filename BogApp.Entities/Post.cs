using MongoDB.Bson;

namespace BogApp.Entities
{
    public record Post : Entity<ObjectId>
    {
        public override ObjectId Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }
    }
}
