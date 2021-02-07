using BlogApp.Common.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class MongoBaseRepository<TEntity, TId> where TEntity : Entity<TId>
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

                entity.Created = DateTime.Now;
                entity.Modified = DateTime.Now;

                await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while creating the object with id: {entity.Id}. Error: {e.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _collection.FindAsync(entity => true, cancellationToken: cancellationToken);
                return result.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while getting all objects. Error: {e.Message}");
                throw;
            }
        }

        public async Task<TEntity> Get(TId id, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _collection.FindAsync<TEntity>(entity => entity.Id.Equals(id), cancellationToken: cancellationToken);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while getting the object with id: {id}. Error: {e.Message}");
                throw;
            }
        }

        public async Task Remove(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.DeleteOneAsync(e => e.Equals(entity), cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while removing the object with id: {entity.Id}. Error: {e.Message}");
                throw;
            }
        }

        public async Task Remove(TId id, CancellationToken cancellationToken)
        {
            try
            {
                await _collection.DeleteOneAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while removing the object with id: {id}. Error: {e.Message}");
                throw;
            }
        }

        public async Task Update(TId id, TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                entity.Id = id;
                entity.Modified = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.Id.Equals(id), entity, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while updating the object with id: {id}. Error: {e.Message}");
                throw;
            }
        }
    }
}
