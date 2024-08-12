using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;

namespace Service.Api.Controllers;

[Route("api/")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger) => _logger = logger;

    /// <summary>
    /// Get all products
    /// </summary>
    /// <param name="_productService"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Products/")]
    [EndpointDescription("Get all products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
        [FromServices] IProductService _productService,
        CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productService.GetProductsAsync(cancellationToken);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting products");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get products filterd by category
    /// </summary>
    /// <param name="category"></param>
    /// <param name="_productService"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Products/Category/{categoryId}")]
    [EndpointDescription("Get products by category")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategoryId(
        [FromRoute] Guid categoryId,
        [FromServices] IProductService _productService,
        CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId, cancellationToken);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting products");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}