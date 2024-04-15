using BloggingApplication.Data;
using BloggingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApplication.Services
{
    public class PostsManager
    {
        private readonly ApiDbContext _context;

        public PostsManager(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post?> GetSinglePost(int id)
        {
            var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                throw new Exception("Invalid Id");
            return post;
        }

        public async Task<int> AddNewPost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post.PostId;
        }

        public async Task PatchPost(int id, Post updatedPost)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                throw new Exception("Invalid Id");

            post.Content = updatedPost.Content;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                throw new Exception("Invalid Id");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

    }
}
