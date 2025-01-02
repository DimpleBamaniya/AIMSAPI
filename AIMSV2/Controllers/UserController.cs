using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AIMSV2.Utility;
using AIMSV2.Models;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("UpdateUserPermissions")]
        public  Task UpdateUserPermissions()
        {
            return _userService.ExecuteUpdateUserPermissions();
        }

        [HttpPost("UpdateUseQuantity")]
        public Task UpdateUseQuantity()
        {
            return _userService.ExecuteUpdateUseQuantity();
        }

        [HttpPost("UpdateAvailableQuantity")]
        public Task UpdateAvailableQuantity()
        {
            return _userService.ExecuteUpdateAvailableQuantity();
        }

        [HttpPost("UpdateAvailableQuantityAndUseQuantity")]
        public Task UpdateAvailableQuantityAndUseQuantity()
        {
            return _userService.ExecuteUpdateAvailableQuantityAndUseQuantity();
        }

        [HttpPost("GetUserDetailByEmailID")]
        public async Task<IActionResult> GetUserDetailByEmailID([FromBody] LoginDto loginRequest)
        {
            Result result = await _userService.GetUserDetailByEmailID(loginRequest);
            return Ok(result.ApiResult);
        }
    }
}
