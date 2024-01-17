using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Service.Abstractions.Models;
using Service.Api.Infrasturcture;
using Service.Api.Services;
using Xunit;

namespace Service.Tests.Services;
public class CategoryServiceTests
{
    [Fact]
    public async Task GetCategoriesAsync_ReturnsCategoryDtos()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var dbContextOptions = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Populate the in-memory database with test data
        using (var context = new ProductDbContext(dbContextOptions))
        {
            context.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category1",
                Description = "Description1"
            });
            context.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category2",
                Description = "Description2"
            });
            context.SaveChanges();
        }

        var dbContext = new ProductDbContext(dbContextOptions);
        var logger = Substitute.For<ILogger<CategoryService>>();
        var categoryService = new CategoryService(logger, dbContext);

        // Act
        var categories = await categoryService.GetCategoriesAsync(cancellationToken);

        // Assert
        Assert.NotNull(categories);
        Assert.Equal(2, categories.Count());
    }
}
