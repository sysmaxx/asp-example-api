using Service.Abstractions.Dtos;

namespace Service.Abstractions.Services;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
}
