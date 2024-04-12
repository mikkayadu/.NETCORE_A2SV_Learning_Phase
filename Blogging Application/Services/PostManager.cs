using Blogging_Application.Models;
using Blogging_Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Application.Services;


public class PostManager
{
    private readonly ApiDbContext _context;

    public PostManager(ApiDbContext context)
    {
        _context = context;
    }



    public async Task<Post> CreatePostAsync(Post post)
    {
        try
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        catch (Exception ex)
        {
            // Log the exception details...
            // Consider rethrowing a custom exception or handling it based on your application needs
            throw new ApplicationException("An error occurred when saving the post.", ex);
        }
    }


    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            // Option 1: Throw a custom NotFoundException
             Console.WriteLine($"Post with ID {id} not found.");
            
            // Option 2: Return null and handle this case in the calling code
            return null;
        }
        return post;
    }

    public async Task<Post> UpdatePostAsync(Post updatedPost)
    {
        var existingPost = await _context.Posts.FindAsync(updatedPost.Id);
        if (existingPost == null)
        {
            // Handle the case where the post doesn't exist.
            throw new ApplicationException($"Post with ID {updatedPost.Id} not found.");
        }

        // Map the updated properties to the existing post
        _context.Entry(existingPost).CurrentValues.SetValues(updatedPost);

        await _context.SaveChangesAsync();
        return existingPost;
    }


    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return false; // Indicate that the operation did not find the resource and hence did not complete.
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true; // Indicate success.
    }






}


