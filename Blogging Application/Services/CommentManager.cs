using Blogging_Application.Data;
using Blogging_Application.Models;
using Microsoft.EntityFrameworkCore;
namespace Blogging_Application.Services;


public class CommentManager
{
    private readonly ApiDbContext _context;

    public CommentManager(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        try
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        catch (Exception ex)
        {
            // Log the exception
            // Consider using a logging framework or ASP.NET Core's built-in logging
            throw new ApplicationException($"Error creating comment: {ex.Message}", ex);
        }
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        try
        {
            return await _context.Comments.ToListAsync();
        }
        catch (Exception ex)
        {
            // Log and handle the exception
            throw new ApplicationException("Error retrieving comments.", ex);
        }
    }



    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        try
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);
            if (comment == null)
            {
                // Option 1: Throw a custom NotFoundException (you'll need to define this exception class)
                // throw new NotFoundException($"Comment with ID {id} not found.");

                // Option 2: Return null and handle this case in the calling code
                return null;
            }
            return comment;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new ApplicationException($"Error retrieving comment with ID {id}.", ex);
        }
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        try
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle concurrency issues, such as when the record was modified or deleted since it was loaded
            throw new ApplicationException("Concurrency error occurred updating the comment.", ex);
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            throw new ApplicationException($"Error updating comment with ID {comment.Id}.", ex);
        }
    }



    public async Task DeleteCommentAsync(int id)
    {
        try
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);
            if (comment == null)
            {
                Console.WriteLine("No such record exist");
                return;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new ApplicationException($"Error deleting comment with ID {id}.", ex);
        }
    }
}

