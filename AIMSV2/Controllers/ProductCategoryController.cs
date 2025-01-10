using AIMSV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIMSV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("GetActiveProductCategories")]
        public IActionResult GetActiveProductCategories()
        {
            var categories = _productCategoryService.GetActiveProductCategories();
            if (!categories.Any())
            {
                return NotFound("No active categories found.");
            }
            return Ok(categories);
        }
    }
}
