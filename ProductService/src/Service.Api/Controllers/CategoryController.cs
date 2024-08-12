using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;

namespace Service.Api.Controllers;
[Route("api/")]
[ApiController]
public class CategoryController : ControllerBase
{
    public CategoryController(ILogger<CategoryController> logger) => _logger = logger;

    private readonly ILogger<CategoryController> _logger;

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <param name="categoryService"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Categories")]
    [EndpointDescription("Get all categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(
        [FromServices] ICategoryService categoryService,
        CancellationToken cancellationToken)
    {
        try
        {
            var categories = await categoryService.GetCategoriesAsync(cancellationToken);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting categories");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
