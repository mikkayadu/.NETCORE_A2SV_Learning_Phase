using BloggingApplication.Data;
using BloggingApplication.Models;
using BloggingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApiDbContext _context;
        CommentManager commentsManager;
        public CommentController(ApiDbContext context)
        {
            _context = context;
            commentsManager = new CommentManager(_context);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await commentsManager.GetAllComments();
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var comment = await commentsManager.GetSingleComment(id);
                return Ok(comment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            try
            {
                var commentId = await commentsManager.AddNewComment(comment);
                return CreatedAtAction(nameof(Get), new { id = commentId }, comment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, Comment updatedComment)
        {
            try
            {
                await commentsManager.PatchComment(id, updatedComment);
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
                await commentsManager.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

