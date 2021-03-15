using BlogApp.Contracts;
using MongoDB.Bson;
using System;

namespace BogApp.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Post(string title, string text, string authorName)
        {
            Id = ObjectId.GenerateNewId(DateTime.UtcNow);
            Title = title;
            Text = text;
            AuthorName = authorName;
            IsDeleted = false;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }
    }
}
