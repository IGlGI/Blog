﻿using BlogApp.Common.Entities;

namespace BogApp.Entities
{
    public record Post : Entity<string>
    {
        public override string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }
    }
}
