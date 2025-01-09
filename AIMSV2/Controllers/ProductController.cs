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
    } 
}
