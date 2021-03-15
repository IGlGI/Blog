using System;

namespace BlogApp.Contracts
{
    public record GetPostResponse(string Id, string Title, string Text, string Author,
        bool IsDeleted, DateTime Created, DateTime Modified);
}