using Microsoft.EntityFrameworkCore;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using Service.Api.Infrasturcture;
using Service.Api.Mapper;

namespace Service.Api.Services;

public class ProductService(
    ILogger<ProductService> logger,
    ProductDbContext productDbContext) : IProductService
{
    private readonly ILogger<ProductService> _logger = logger;
    private readonly ProductDbContext _productDbContext = productDbContext;

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productDbContext
            .Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);

        return products.Select(product => product.ToProductDto());
    }

}
