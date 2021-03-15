using BlogApp.Contracts;
using BlogApp.Infrastructure.Repositories.Interfaces;
using BogApp.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class PostRepository : MongoBaseRepository<Post>, IPostRepository
    {

        public PostRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory, "Posts")
        { }

        public new async Task<string> Create(Post post, CancellationToken cancellationToken)
        {
            await base.Create(post, cancellationToken);
            return post.Id.ToString();
        }

        public async Task<Post> Get(string id, CancellationToken cancellationToken)
        {
            return await base.Get(x => x.Id == new ObjectId(id), cancellationToken);
        }

        public new async Task<IEnumerable<Post>> Get(CancellationToken cancellationToken)
        {
            return await base.Get(cancellationToken);
        }

        public async Task<bool> Remove(string id, CancellationToken cancellationToken)
        {
            return await base.Remove(x => x.Id == new ObjectId(id), cancellationToken);
        }

        public async Task<bool> Update(string id, PostRequest request, CancellationToken cancellationToken)
        {
            var post = await Get(id, cancellationToken);
            if (post == null) return false;

            post.AuthorName = request.AuthorName;
            post.Text = request.Text;
            post.Title = request.Title;
            post.Modified = DateTime.UtcNow;

            await Update(id, post, cancellationToken);
            return true;
        }

        public async Task Update(string id, Post post, CancellationToken cancellationToken)
        {
            await base.Update(x => x.Id == new ObjectId(id), post, cancellationToken);
        }
    }
}
