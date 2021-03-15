using BlogApp.Contracts;
using BogApp.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post, string>
    {
        public Task<bool> Update(string id, PostRequest entity, CancellationToken cancellationToken);
    }
}
