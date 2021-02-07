using BlogApp.Common.Entities;
using MongoDB.Bson;

namespace BlogApp.Domain
{
    public sealed record PostDto : Entity<ObjectId>
    {
        public override ObjectId Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }
    }
}
