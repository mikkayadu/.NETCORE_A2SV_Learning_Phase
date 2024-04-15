using BloggingApplication.Data;
using BloggingApplication.Models;
using BloggingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApiDbContext _context;
        PostsManager postsManager;

        public PostController(ApiDbContext context)
        {
            _context = context;
            postsManager = new PostsManager(_context);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postsManager.GetAllPosts();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var postId = await postsManager.AddNewPost(post);
            return CreatedAtAction(nameof(Get), new { id = postId }, post);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var post = await postsManager.GetSinglePost(id);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, Post updatedPost)
        {
            try
            {
                await postsManager.PatchPost(id, updatedPost);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await postsManager.DeletePost(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
    }
}


