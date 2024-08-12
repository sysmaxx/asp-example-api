using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Service.Abstractions.Models;
using Service.Api.Infrasturcture;
using Service.Api.Services;
using Xunit;

namespace Service.Tests.Services;
public class ProductServiceTests
{
    [Fact]
    public async Task GetProductsAsync_ReturnsProductDtos()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var dbContextOptions = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Populate the in-memory database with test data
        using (var context = new ProductDbContext(dbContextOptions))
        {
            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Description = "Description1",
                Price = 10.0,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid(), Name = "Category1" }
            });
            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                Description = "Description2",
                Price = 20.0,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid(), Name = "Category2" }
            });
            
            await context.SaveChangesAsync();
        }

        var dbContext = new ProductDbContext(dbContextOptions);
        var logger = Substitute.For<ILogger<ProductService>>();
        var productService = new ProductService(logger, dbContext);

        // Act
        var products = await productService.GetProductsAsync(cancellationToken);

        // Assert
        Assert.NotNull(products);
        Assert.Equal(2, products.Count());
    }

    [Fact]
    public async Task GetProductsByCategoryIdAsync_ReturnsProductDtos()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryNotExpectedId = Guid.NewGuid();

        var cancellationToken = CancellationToken.None;

        var dbContextOptions = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Populate the in-memory database with test data
        using (var context = new ProductDbContext(dbContextOptions))
        {
            var category = new Category { Id = categoryId, Name = "Category1" };
            var categoryNotExpected = new Category { Id = categoryNotExpectedId, Name = "Category2" };

            context.Categories.Add(category);
            context.Categories.Add(categoryNotExpected);

            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Description = "Description1",
                Price = 10.0,
                Category = category
            });

            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                Description = "Description2",
                Price = 10.0,

                Category = category
            });

            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product3",
                Description = "Description3",
                Price = 30.0,
                Category = categoryNotExpected
            });
            
            await context.SaveChangesAsync();
        }

        var dbContext = new ProductDbContext(dbContextOptions);
        var logger = Substitute.For<ILogger<ProductService>>();
        var productService = new ProductService(logger, dbContext);

        // Act
        var products = await productService.GetProductsByCategoryIdAsync(categoryId, cancellationToken);

        // Assert
        Assert.NotNull(products);
        Assert.Equal(2, products.Count());
    }
}