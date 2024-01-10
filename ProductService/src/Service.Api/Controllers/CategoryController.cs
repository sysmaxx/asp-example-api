using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Services;

namespace Service.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(ILogger<CategoryController> logger) : ControllerBase
{
    private readonly ILogger<CategoryController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult> GetCategories(
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
