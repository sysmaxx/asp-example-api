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
public class CategoriesControllerTests
{
    [Fact]
    public async Task GetCategories_ReturnsOkResult_WithCategories()
    {
        // Arrange
        var categoryService = Substitute.For<ICategoryService>();
        var cancellationToken = CancellationToken.None;
        var logger = Substitute.For<ILogger<CategoryController>>();

        var expectedCategories = new List<CategoryDto>
        {
            new CategoryDto { Id = Guid.NewGuid(), Name = "Category1", Description = "Description1" },
            new CategoryDto { Id = Guid.NewGuid(), Name = "Category2", Description = "Description2" },
        };

        categoryService.GetCategoriesAsync(cancellationToken).Returns(expectedCategories);

        var controller = new CategoryController(logger);

        // Act
        var response = await controller.GetCategories(categoryService, cancellationToken);

        // Assert
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(GetObjectResultContent(response));
        Assert.Equal(StatusCodes.Status200OK, (response.Result as OkObjectResult)?.StatusCode);
        Assert.Equal(expectedCategories.Count, GetObjectResultContent(response).Count());
    }

    [Fact]
    public async Task GetCategories_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var categoryService = Substitute.For<ICategoryService>();
        var cancellationToken = CancellationToken.None;
        var logger = Substitute.For<ILogger<CategoryController>>();

        categoryService.GetCategoriesAsync(cancellationToken).Throws(new Exception("Simulated error"));

        var controller = new CategoryController(logger);

        // Act
        var response = await controller.GetCategories(categoryService, cancellationToken);

        // Assert
        Assert.IsType<StatusCodeResult>(response.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, (response.Result as StatusCodeResult)?.StatusCode);
    }
}
