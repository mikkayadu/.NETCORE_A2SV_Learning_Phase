using System;
using System.Threading.Tasks;
using BloggingApplication.Data;
using BloggingApplication.Models;
using BloggingApplication.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PostsManagerTests
{
    private readonly ApiDbContext _context;
    private readonly PostsManager _manager;

    public PostsManagerTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbnn")
            .Options;

        _context = new ApiDbContext(options);
        _manager = new PostsManager(_context);
    }

    [Fact]
    public async Task GetAllPosts_ReturnsAllPosts()
    {
        _context.Posts.AddRange(
            new Post { Title = "dldl", PostId = 1, Content = "Test Post 1" },
            new Post { Title = "clc", PostId = 2, Content = "Test Post 2" }
        );
        await _context.SaveChangesAsync();

        var posts = await _manager.GetAllPosts();

        Xunit.Assert.Equal(2, posts.Count);
    }

    [Fact]
    public async Task GetSinglePost_ReturnsPost_WhenPostExists()
    {
        var expectedPost = new Post { Title = "suuii", PostId = 78, Content = "Test Post 1" };
        _context.Posts.Add(expectedPost);
        await _context.SaveChangesAsync();

        var result = await _manager.GetSinglePost(78);

        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(expectedPost.Content, result.Content);
    }

    [Fact]
    public async Task GetSinglePost_ThrowsException_WhenPostDoesNotExist()
    {
        await Xunit.Assert.ThrowsAsync<Exception>(() => _manager.GetSinglePost(99));
    }

    [Fact]
    public async Task AddNewPost_AddsPost()
    {
        var newPost = new Post { Title = "ldldd", Content = "New Post" };

        var postId = await _manager.AddNewPost(newPost);
        var post = await _context.Posts.FindAsync(postId);

        Xunit.Assert.NotNull(post);
        Xunit.Assert.Equal("New Post", post.Content);
    }

    [Fact]
    public async Task PatchPost_UpdatesContent_WhenPostExists()
    {
        var existingPost = new Post { PostId = 1, Title = "ldldd", Content = "Old Content" };
        _context.Posts.Add(existingPost);
        await _context.SaveChangesAsync();

        await _manager.PatchPost(1, new Post { Content = "Updated Content" });
        var updatedPost = await _context.Posts.FindAsync(1);

        Xunit.Assert.Equal("Updated Content", updatedPost.Content);
    }

    [Fact]
    public async Task PatchPost_ThrowsException_WhenPostDoesNotExist()
    {
        await Xunit.Assert.ThrowsAsync<Exception>(() => _manager.PatchPost(4, new Post { PostId = 4, Title = "ldldd", Content = "New Post" }));
    }

    [Fact]
    public async Task DeletePost_DeletesPost_WhenPostExists()
    {
        var postToDelete = new Post { PostId = 1, Title = "ldldd", Content = "New Post" };
        _context.Posts.Add(postToDelete);
        await _context.SaveChangesAsync();

        await _manager.DeletePost(1);
        var post = await _context.Posts.FindAsync(1);

        Xunit.Assert.Null(post);
    }

    [Fact]
    public async Task DeletePost_ThrowsException_WhenPostDoesNotExist()
    {
        await Xunit.Assert.ThrowsAsync<Exception>(() => _manager.DeletePost(99));
    }
}
