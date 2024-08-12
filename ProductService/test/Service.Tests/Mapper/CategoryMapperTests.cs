using Service.Abstractions.Models;
using Service.Api.Mapper;
using Xunit;

namespace Service.Tests.Mapper;
public class CategoryMapperTests
{

    [Fact]
    public void ToCategoryDto_CorrectConversion_ReturnsCategoryDto()
    {
        // Arrange
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Created = DateTime.Now,
            Description = "All electronic items"
            
        };

        // Act
        var result = category.ToCategoryDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal(category.Name, result.Name);
        Assert.Equal(category.Created, result.Created);
        Assert.Equal(category.Description, result.Description);
    }

    [Fact]
    public void ToCategoryDto_NullCategory_ThrowsArgumentNullException()
    {
        // Arrange
        Category category = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => category.ToCategoryDto());
    }

    [Fact]
    public void ToCategoryDto_DefaultGuid_IdIsDefaultGuid()
    {
        // Arrange
        var category = new Category
        {
            Name = "Books",
            Description = "All kinds of books"
        };

        // Act
        var result = category.ToCategoryDto();

        // Assert
        Assert.Equal(Guid.Empty, result.Id);
    }
}
