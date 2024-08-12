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

    /// <summary>
    /// Get all products
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productDbContext
            .Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);

        return products.Select(product => product.ToProductDto());
    }

    /// <summary>
    /// Get products filterd by category
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = await _productDbContext
            .Products
            .Include(p => p.Category)
            .Where(p => p.Category.Id == categoryId)
            .ToListAsync(cancellationToken);

        return products.Select(product => product.ToProductDto());
    }
}