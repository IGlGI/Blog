using BlogApp.DataAccess.Repositories.Interfaces;
using BogApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repositories
{
    public abstract class MongoBaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoBaseRepository(AppSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.DbName);
            var database = client.GetDatabase(settings.DbName);
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task Create(TEntity entity, CancellationToken cancellationToken = default) =>
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken = default)
        {
            var result = await _collection.FindAsync(entity => true, cancellationToken: cancellationToken);
            return result.ToList();
        }

        public async Task<TEntity> Get(TId id, CancellationToken cancellationToken = default)
        {
            var result = await _collection.FindAsync<TEntity>(entity => entity.Id.Equals(id), cancellationToken: cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task Remove(TEntity entity, CancellationToken cancellationToken) =>
            await _collection.DeleteOneAsync(e => e.Id.Equals(entity.Id), cancellationToken: cancellationToken);

        public async Task Remove(TId id, CancellationToken cancellationToken) =>
            await _collection.DeleteOneAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);

        public async Task Update(TId id, TEntity entity, CancellationToken cancellationToken) =>
            await _collection.ReplaceOneAsync(e => e.Id.Equals(id), entity, cancellationToken: cancellationToken);
    }
}
