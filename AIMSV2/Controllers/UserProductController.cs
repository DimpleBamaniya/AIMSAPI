using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AIMSV2.Models;
using AutoMapper;
using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserProductService _userProductService;

        public UserProductController(IUserProductService userProductService)
        {
            _userProductService = userProductService;
        }

        [HttpGet("GetAllUserProduct")]
        public async Task<IActionResult> GetAllUserProduct()
        {
            var users = await _userProductService.GetAllUserProduct();
            return Ok(users);
        }

        [HttpGet("GetAllUserProduct1")]
        public async Task<IActionResult> GetAllUserProduct1()
        {
            var userProducts = await _userProductService.GetAllUserProductAsync();
            return Ok(userProducts);
        }

        [HttpPost("GetProductListbyUserID")]
        public async Task<IActionResult> GetProductListbyUserID([FromBody] UserIdRequest userIdRequest)
        {
            Result result = await _userProductService.GetProductListbyUserID(userIdRequest.ID);
            return Ok(result.ApiResult);
        }

        [HttpPost("DeleteUserProduct")]
        public async Task<IActionResult> DeleteUserProduct([FromBody] UserProductIdRequest userProductIdRequest)
        {
            var result = await _userProductService.DeleteUserProductAsync(userProductIdRequest.ID);

            if (result)
                return Ok(new { Message = "UserProduct deleted successfully." });

            return NotFound(new { Message = "UserProduct not found." });
        }

        [HttpPost("SaveUserProducts")]
        public async Task<IActionResult> SaveUserProducts([FromBody] SaveUserProduct userProductModel)
        {
            Result result = await _userProductService.SaveUserProducts(userProductModel);
            return Ok(result.ApiResult);
        }

        [HttpGet("check-user-product-category")]
        public async Task<IActionResult> CheckUserProductCategoryMatch([FromQuery] int userId, [FromQuery] int categoryId)
        {
            try
            {
                // Call the service to check if the user and category match
                var result = await _userProductService.CheckUserProductCategoryMatchAsync(userId, categoryId);

                // Check if the service call was successful
                if (result.Status == StatusType.Success)
                {
                    // Return Ok response with success status and match result
                    return Ok(result.ApiResult); // Use ApiResult to return the response
                }

                // If there is no match or other failure, return the error details from the result
                return StatusCode(result.StatusCode, result.ApiResult);
            }
            catch (Exception ex)
            {
                // Return a 500 error if an exception occurred
                return StatusCode(500, new Result("An error occurred while processing your request.", "InternalServerError", HttpStatusCode.InternalServerError, ex).ApiResult);
            }
        }
    }
}

