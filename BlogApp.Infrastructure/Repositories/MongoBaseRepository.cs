using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class MongoBaseRepository<TEntity>
    {
        private readonly ILogger _logger;

        private readonly IMongoCollection<TEntity> _collection;

        public MongoBaseRepository(IMongoDatabase database, ILoggerFactory loggerFactory, string collectionName)
        {
            _logger = loggerFactory.CreateLogger(collectionName);
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating the object");
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _collection.FindAsync(entity => true, cancellationToken: cancellationToken);
                return result.ToList(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting all objects");
                throw;
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _collection.FindAsync(expression, cancellationToken: cancellationToken);
                return result.FirstOrDefault(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting the object");
                throw;
            }
        }

        public async Task<bool> Remove(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var deleted = await _collection.DeleteOneAsync(expression, cancellationToken: cancellationToken);
                return deleted.DeletedCount != 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while removing the object");
                throw;
            }
        }

        public async Task Update(Expression<Func<TEntity, bool>> expression, TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _collection.ReplaceOneAsync(expression, entity, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while updating the object");
                throw;
            }
        }
    }
}
