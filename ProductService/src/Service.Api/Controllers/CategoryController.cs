using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;

namespace Service.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    public CategoryController(ILogger<CategoryController> logger) => _logger = logger;

    private readonly ILogger<CategoryController> _logger;

    [HttpGet]
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
