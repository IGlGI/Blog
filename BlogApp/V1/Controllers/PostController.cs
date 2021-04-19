using BlogApp.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Contracts;
using Microsoft.AspNetCore.Http;

namespace BlogApp.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/[controller]")]
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostRequest postRequest, CancellationToken cancellationToken)
        {
            var created = await _postRepository.Create(postRequest, cancellationToken);
            return Ok(created);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var posts = await _postRepository.Get(cancellationToken);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(id, cancellationToken);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PostRequest postRequest, CancellationToken cancellationToken)
        {
            if (await _postRepository.Update(id, postRequest, cancellationToken))
            {
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (await _postRepository.Remove(id, cancellationToken))
            {
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}
