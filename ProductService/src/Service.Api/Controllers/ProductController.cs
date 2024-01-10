using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Services;

namespace Service.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ILogger<ProductController> logger) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult> GetProducts(
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
}