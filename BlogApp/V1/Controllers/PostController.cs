using BlogApp.DataAccess.Repositories.Interfaces;
using BogApp.Models;
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
        private readonly IRepository<Post, string> _postRepository;

        public PostController(IRepository<Post, string> postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get(CancellationToken cancellationToken) =>
            await _postRepository.Get(cancellationToken);

        [HttpGet("{id}")]
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
            await _postRepository.Create(post, cancellationToken);

            return CreatedAtRoute("Get", new { id = post.Id }, post);
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

            return NoContent();
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

            return NoContent();
        }
    }
}
