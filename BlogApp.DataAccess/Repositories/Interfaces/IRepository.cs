using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity, TId>
    {
        public Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken);

        public Task<TEntity> Get(TId id, CancellationToken cancellationToken);

        public Task Create(TEntity entity, CancellationToken cancellationToken);

        public Task Update(TId id, TEntity entity, CancellationToken cancellationToken);

        public Task Remove(TEntity entity, CancellationToken cancellationToken);

        public Task Remove(TId id, CancellationToken cancellationToken);
    }
}
