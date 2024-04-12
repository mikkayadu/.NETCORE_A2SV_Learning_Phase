using Blogging_Application.Data;
using Blogging_Application.Models;
using Blogging_Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BloggingApplicationTest.Services
{
    public class PostManagerTests
    {
        private readonly DbContextOptions<ApiDbContext> _dbContextOptions;

        public PostManagerTests()
        {
            // Setup DbContextOptions for creating an instance of ApiDbContext with InMemory database for each test
            _dbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbbb") // Ensure a unique name to avoid conflicts between tests
                .Options;
        }

        [Fact]
        public async Task CreatePostAsync_SavesPostCorrectly()
        {
            // Arrange
            using var context = new ApiDbContext(_dbContextOptions);
            var service = new PostManager(context);
            var post = new Post { Title = "Test Post", Content = "Test Content" };
            
            // Act
            await service.CreatePostAsync(post);

            // Assert
            var savedPost = await context.Posts.FirstOrDefaultAsync();
            Xunit.Assert.NotNull(savedPost);
            Xunit.Assert.Equal("Test Post", savedPost.Title);
            Xunit.Assert.Equal("Test Content", savedPost.Content);
        }


        // Additional tests for other methods and scenarios...



        private ApiDbContext GetContextWithInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabaseForTestingb") // Use a unique name to avoid conflicts
                .Options;
            var context = new ApiDbContext(options);

            return context;
        }

        [Fact]
        public async Task UpdatePostAsync_UpdatesExistingPost()
        {
            // Arrange
            var context = GetContextWithInMemoryDb();
            var service = new PostManager(context);
            var testPost = new Post { Id = 2, Title = "Original Title", Content = "Original Content" };

            // Simulate adding a post to the database
            context.Posts.Add(testPost);
            await context.SaveChangesAsync();

            // Act
            var updatedPost = new Post { Id = 2, Title = "Updated Title", Content = "Updated Content" };
            await service.UpdatePostAsync(updatedPost);

            // Assert
            var resultPost = await context.Posts.FirstOrDefaultAsync(p => p.Id == 2);
            Xunit.Assert.NotNull(resultPost);
            Xunit.Assert.Equal("Updated Title", resultPost.Title);
            Xunit.Assert.Equal("Updated Content", resultPost.Content);
        }

        private ApiDbContext GetNewInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            return new ApiDbContext(options);
        }

        [Fact]
        public async Task DeletePostAsync_ReturnsTrue_WhenPostExists()
        {
            using var context = GetNewInMemoryDbContext();
            var service = new PostManager(context);

            // Arrange - Add a post to delete
            var newPost = new Post { Title = "Test Post", Content = "Test Content" };
            context.Posts.Add(newPost);
            await context.SaveChangesAsync();

            // Act
            var result = await service.DeletePostAsync(newPost.Id);

            // Assert
            Xunit.Assert.True(result);
            Xunit.Assert.Null(await context.Posts.FindAsync(newPost.Id));
        }

        [Fact]
        public async Task DeletePostAsync_ReturnsFalse_WhenPostDoesNotExist()
        {
            using var context = GetNewInMemoryDbContext();
            var service = new PostManager(context);

            // Act
            var result = await service.DeletePostAsync(-1); // Assuming -1 is a non-existent ID

            // Assert
            Xunit.Assert.False(result);
        }

    }
}
