using Microsoft.EntityFrameworkCore;
using Blogging_Application.Data;
using Blogging_Application.Models;
using Blogging_Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace BloggingApplicationTest
{
    [TestClass]
    public class PostManagerTests
    {
        [TestMethod]
        public async Task CreatePostAsync_SavesToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "CreatePostTestDb") // Ensure a unique name for each test
                .Options;

            // Act
            using (var context = new ApiDbContext(options))
            {
                var service = new PostManager(context); // Adjust if your constructor differs
                await service.CreatePostAsync(new Post { Title = "Happy Holidays", Content = "Ramadan Mubarak to everyone" });
            }

            // Assert
            using (var context = new ApiDbContext(options))
            {
                var count = await context.Posts.CountAsync();
                Assert.AreEqual(1, count); // Verifies that one post exists

                var post = await context.Posts.FirstOrDefaultAsync();
                Assert.IsNotNull(post); // Verifies that a post was retrieved
                Assert.AreEqual("Happy Holidays", post.Title); // Verifies the title
                Assert.AreEqual("Ramadan Mubarak to everyone", post.Content); // Verifies the content
            }
        }
    }
}

