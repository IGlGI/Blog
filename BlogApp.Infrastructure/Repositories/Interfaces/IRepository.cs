using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity, TId>
    {
        public Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken);

        public Task<TEntity> Get(TId id, CancellationToken cancellationToken);

        public Task<TId> Create(TEntity entity, CancellationToken cancellationToken);

        public Task Update(TId id, TEntity entity, CancellationToken cancellationToken);

        public Task<bool> Remove(TId id, CancellationToken cancellationToken);
    }
}
