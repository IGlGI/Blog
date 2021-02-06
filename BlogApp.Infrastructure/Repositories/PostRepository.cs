using BlogApp.Infrastructure.Repositories.Interfaces;
using BogApp.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class PostRepository : MongoBaseRepository<Post, ObjectId>, IPostRepository
    {
        public PostRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory, "Posts")
        {
        }

        public async Task<ObjectId> Create(Post entity, CancellationToken cancellationToken)
        {
            entity.Id = ObjectId.GenerateNewId(DateTime.Now);
            await base.Create(entity, cancellationToken);

            return entity.Id;
        }
    }
}
