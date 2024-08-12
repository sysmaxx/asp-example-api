using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using Service.Api.Controllers;
using Xunit;
using static Service.Tests.Extensions.ActionResultExtension;

namespace Service.Tests.Controllers;
public class ProductControllerTests
{

    [Fact]
    public async Task GetProductsAsync_ReturnsOkResult_WithProducts()
    {
        // Arrange
        var productService = Substitute.For<IProductService>();
        var cancellationToken = CancellationToken.None;

        var expectedProducts = new List<ProductDto>
        {
            new ProductDto { Id = Guid.NewGuid(), Name = "Product1", Description = "Description1", CategoryName = "Category1", Price = 10.0 },
            new ProductDto { Id = Guid.NewGuid(), Name = "Product2", Description = "Description2", CategoryName = "Category2", Price = 20.0 },
        };

        productService.GetProductsAsync(cancellationToken).Returns(expectedProducts);

        var logger = Substitute.For<ILogger<ProductController>>();
        var controller = new ProductController(logger);

        // Act
        var response = await controller.GetProducts(productService, cancellationToken);

        // Assert
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(GetObjectResultContent(response));
        Assert.Equal(StatusCodes.Status200OK, (response.Result as OkObjectResult)?.StatusCode);
        Assert.Equal(expectedProducts.Count, GetObjectResultContent(response).Count());
    }

    [Fact]
    public async Task GetProducts_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var productService = Substitute.For<IProductService>();
        var cancellationToken = CancellationToken.None;

        productService.GetProductsAsync(cancellationToken).Throws(new Exception("Simulated error"));

        var logger = Substitute.For<ILogger<ProductController>>();
        var controller = new ProductController(logger);

        // Act
        var response = await controller.GetProducts(productService, cancellationToken);

        // Assert
        Assert.IsType<StatusCodeResult>(response.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, (response.Result as StatusCodeResult)?.StatusCode);
    }

    [Fact]
    public async Task GetProductsByCategoryId_ReturnsOkResult_WithProducts()
    {
        // Arrange
        var productService = Substitute.For<IProductService>();
        var cancellationToken = CancellationToken.None;
        var categoryId = Guid.NewGuid();

        var expectedProducts = new List<ProductDto>
        {
            new ProductDto { Id = Guid.NewGuid(), Name = "Product1", Description = "Description1", CategoryName = "Category1", Price = 10.0 },
            new ProductDto { Id = Guid.NewGuid(), Name = "Product2", Description = "Description2", CategoryName = "Category2", Price = 20.0 },
        };

        productService.GetProductsByCategoryIdAsync(categoryId, cancellationToken).Returns(expectedProducts);

        var logger = Substitute.For<ILogger<ProductController>>();
        var controller = new ProductController(logger);

        // Act
        var response = await controller.GetProductsByCategoryId(categoryId, productService, cancellationToken);

        // Assert
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(GetObjectResultContent(response));
        Assert.Equal(StatusCodes.Status200OK, (response.Result as OkObjectResult)?.StatusCode);
        Assert.Equal(expectedProducts.Count, GetObjectResultContent(response).Count());
    }
}
