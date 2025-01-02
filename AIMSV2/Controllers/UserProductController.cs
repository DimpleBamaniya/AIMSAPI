using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AutoMapper;

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
    }
}

