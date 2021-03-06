using AutoMapper;
using BlogApp.Domain;
using BlogApp.Infrastructure.Repositories.Interfaces;
using BogApp.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class PostRepository : MongoBaseRepository<PostDto, ObjectId>, IPostRepository
    {
        private readonly IMapper _mapper;

        public PostRepository(IMapper mapper, IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory, "Posts")
        {
            _mapper = mapper;
        }

        public async Task<string> Create(Post post, CancellationToken cancellationToken)
        {
            var postDto = _mapper.Map<PostDto>(post);
            postDto.Id = ObjectId.GenerateNewId(DateTime.Now);

            await base.Create(postDto, cancellationToken);
            return postDto.Id.ToString();
        }

        public async Task<Post> Get(string id, CancellationToken cancellationToken)
        {
            var post = await base.Get(new ObjectId(id), cancellationToken);

            if (post == null)
            {
                return null;
            }
            return _mapper.Map<Post>(post);
        }

        public new async Task<IEnumerable<Post>> Get(CancellationToken cancellationToken)
        {
            var postDto = await base.Get(cancellationToken);

            var postDtoArray = postDto as PostDto[] ?? postDto.ToArray();
            if (!postDtoArray.Any())
            {
                return null;
            }
            var posts = new List<Post>();
            postDtoArray.ToList().ForEach(dto =>
            {
                posts.Add(_mapper.Map<Post>(dto));
            });

            return posts;
        }

        public async Task Remove(Post post, CancellationToken cancellationToken)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await base.Remove(postDto, cancellationToken);
        }

        public async Task Remove(string id, CancellationToken cancellationToken)
        {
            await base.Remove(new ObjectId(id), cancellationToken);
        }

        public async Task Update(string id, Post post, CancellationToken cancellationToken)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await base.Update(new ObjectId(id), postDto, cancellationToken);
        }
    }
}
