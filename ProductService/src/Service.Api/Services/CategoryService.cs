using Microsoft.EntityFrameworkCore;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using Service.Api.Infrasturcture;
using Service.Api.Mapper;

namespace Service.Api.Services;

public class CategoryService(
           ILogger<CategoryService> logger,
           ProductDbContext productDbContext) : ICategoryService
{
    private readonly ILogger<CategoryService> _logger = logger;
    private readonly ProductDbContext _productDbContext = productDbContext;

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await _productDbContext
            .Categories
            .ToListAsync(cancellationToken);

        return categories.Select(category => category.ToCategoryDto());
    }
}
