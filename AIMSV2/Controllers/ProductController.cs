using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("GetAllProductDetails")]
    [Authorize]
    public async Task<IActionResult> GetAllProductDetails([FromBody] Pagination pagination)
    {
        Result result = await _productService.GetAllProductDetailsAsync(pagination);
        return Ok(result.ApiResult);
    }

    [HttpPost("SaveProduct")]
    [Authorize]
    public async Task<IActionResult> SaveProduct(SaveProductDto productModel)
    {
        Result result = await _productService.SaveProductAsync(productModel);
        return Ok(result.ApiResult);
    }

    [HttpPost("GetUserListByProductID")]
    [Authorize]
    public async Task<IActionResult> GetUserListByProductID([FromBody] ProductIdRequest productIdRequest)
    {
        Result result = await _productService.GetUserListByProductIDAsync(productIdRequest.Id);
        return Ok(result.ApiResult);
    }

    [HttpPost("DeleteProduct")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductDto productModel)
    {
        Result result = await _productService.DeleteProductAsync(productModel);
        return Ok(result.ApiResult);
    }

}
