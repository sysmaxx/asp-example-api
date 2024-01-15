using Microsoft.EntityFrameworkCore;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using Service.Api.Infrasturcture;
using Service.Api.Mapper;

namespace Service.Api.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly ProductDbContext _productDbContext;

    public ProductService(
    ILogger<ProductService> logger,
    ProductDbContext productDbContext)
    {
        _logger = logger;
        _productDbContext = productDbContext;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productDbContext
            .Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);

        return products.Select(product => product.ToProductDto());
    }
}