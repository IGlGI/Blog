using BlogApp.Infrastructure.Repositories.Interfaces;
using BogApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Post>> Get(CancellationToken cancellationToken)
        {
            return await _postRepository.Get(cancellationToken);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> Get(string id, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(id, cancellationToken);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post post, CancellationToken cancellationToken)
        {
            var postId = await _postRepository.Create(post, cancellationToken);

            if (string.IsNullOrWhiteSpace(postId.ToString()))
            {
                return NotFound();
            }

            return await _postRepository.Get(postId, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Post postIn, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(id, cancellationToken);

            if (post == null)
            {
                return NotFound();
            }

            await _postRepository.Update(id, postIn, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(id, cancellationToken);

            if (post == null)
            {
                return NotFound();
            }

            await _postRepository.Remove(post.Id, cancellationToken);

            return Ok();
        }
    }
}
