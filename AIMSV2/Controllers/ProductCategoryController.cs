using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpGet("GetActiveProductCategories")]
    [Authorize]
    public async Task<IActionResult> GetActiveProductCategories()
    {
        Result result = await _productCategoryService.GetActiveProductCategoriesAsync();
        return Ok(result.ApiResult);
    }
}
