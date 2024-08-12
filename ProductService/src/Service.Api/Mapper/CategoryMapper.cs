using Service.Abstractions.Dtos;
using Service.Abstractions.Models;

namespace Service.Api.Mapper;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Created = category.Created,
            Description = category.Description
        };
    }
}
