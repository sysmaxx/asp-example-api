using Service.Abstractions.Dtos;

namespace Service.Abstractions.Services;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);
}
