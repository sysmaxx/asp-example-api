using Service.Abstractions.Models;
using Service.Api.Mapper;
using Xunit;

namespace Service.Tests.Mapper;
public class ProductMapperTests
{

    [Fact]
    public void ToProductDto_WithValidProduct_ReturnsValidProductDto()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Test Description",
            Price = 19.99,
            Category = new Category
            {
                Name = "TestCategory"
            }
        };

        // Act
        var productDto = product.ToProductDto();

        // Assert
        Assert.NotNull(productDto);
        Assert.Equal(product.Id, productDto.Id);
        Assert.Equal(product.Name, productDto.Name);
        Assert.Equal(product.Description, productDto.Description);
        Assert.Equal(product.Price, productDto.Price);
        Assert.Equal(product.Category.Name, productDto.CategoryName);
    }

    [Fact]
    public void ToProductDto_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        Product product = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => product.ToProductDto());
    }
}

