using Microsoft.AspNetCore.Mvc;
using Blogging_Application.Services;
using Blogging_Application.Models;
using System.Threading.Tasks;



[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly PostManager _postManager;
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostByIdAsync(int id)
    {
        var post = await _postManager.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return post;
    }

    public PostsController(PostManager postManager)
    {
        _postManager = postManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPost = await _postManager.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostByIdAsync), new { id = createdPost.Id }, createdPost);
    }

    // Add other controller actions here...
}
