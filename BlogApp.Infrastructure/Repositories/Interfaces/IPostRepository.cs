using BogApp.Entities;
using MongoDB.Bson;

namespace BlogApp.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post, string>
    {
    }
}
