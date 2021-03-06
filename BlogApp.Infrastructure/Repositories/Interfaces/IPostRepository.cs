using BogApp.Entities;

namespace BlogApp.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post, string>
    {
    }
}
