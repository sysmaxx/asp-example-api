using Service.Abstractions.Dtos;
using Service.Abstractions.Models;

namespace Service.Api.Mapper;

public static class ProductMapper
{
    public static ProductDto ToProductDto(this Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryName = product.Category.Name
        };
    }
}
