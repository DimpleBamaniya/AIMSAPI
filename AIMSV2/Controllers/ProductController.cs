using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AIMSV2.Models;
using AIMSV2.Utility;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("UpdateUseQuantity")]
        public Task UpdateUseQuantity()
        {
            return _productService.ExecuteUpdateUseQuantity();
        }

        [HttpPost("UpdateAvailableQuantity")]
        public Task UpdateAvailableQuantity()
        {
            return _productService.ExecuteUpdateAvailableQuantity();
        }

        [HttpPost("UpdateAvailableQuantityAndUseQuantity")]
        public Task UpdateAvailableQuantityAndUseQuantity()
        {
            return _productService.ExecuteUpdateAvailableQuantityAndUseQuantity();
        }

        [HttpPost("GetProductDetailByID")]
        public async Task<IActionResult> GetProductDetailByID([FromBody] ProductIdRequest productIdRequest)
        {
            Result result = await _productService.GetProductDetailByID(productIdRequest.Id);
            return Ok(result.ApiResult);
        }

        [HttpPost("GetAllProductDetails")]
        public async Task<IActionResult> GetAllProductDetails([FromBody] Pagination pagination)
        {
            Result result = await _productService.GetAllProductDetails(pagination);
            return Ok(result.ApiResult);
        }

       // Endpoint to check if product exists
    [HttpGet("CheckProductExists")]
    public async Task<IActionResult> CheckProductExists(int categoryId, int brandId)
    {
        var result = await _productService.CheckIfProductExistsAsync(categoryId, brandId);
        return Ok(result); // Will return true or false based on stored procedure result
    }

    [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct(SaveProductDto productModel)
        {
            Result result = await _productService.SaveProduct(productModel);
            return Ok(result.ApiResult);
        }

        [HttpPost("GetUserListByProductID")]
        public async Task<IActionResult> GetUserListByProductID([FromBody] ProductIdRequest productIdRequest)
        {
            Result result = await _productService.GetUserListByProductID(productIdRequest.Id);
            return Ok(result.ApiResult);
        }
        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductDto productModel)
        {
            Result result = await _productService.DeleteProduct(productModel);
            return Ok(result.ApiResult);
        }
    }
}
