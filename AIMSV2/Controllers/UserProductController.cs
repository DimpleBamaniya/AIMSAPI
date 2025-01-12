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
            Result result = await _userProductService.GetProductListbyUserID(userIdRequest.Id);
            return Ok(result.ApiResult);
        }
    }
}

