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
        
        [HttpPost("GetUserDetailByEmailID")]
        public async Task<IActionResult> GetUserDetailByEmailID([FromBody] LoginDto loginRequest)
        {
            Result result = await _userService.GetUserDetailByEmailID(loginRequest);
            return Ok(result.ApiResult);
        }

        [HttpPost("ExecuteUpdateUserCodes")]
        public Task ExecuteUpdateUserCodes()
        {
            return _userService.ExecuteUpdateUserCodes();
        }

        [HttpPost("GetUserDetailByID")]
        public async Task<IActionResult> GetUserDetailByID([FromBody] UserIdRequest userIdRequest)
        {
            Result result = await _userService.GetUserDetailByID(userIdRequest.ID);
            return Ok(result.ApiResult);
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser(UserDto userModel)
        {
            Result result = await _userService.SaveUser(userModel);
            return Ok(result.ApiResult);
        }

        [HttpPost("GetAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails([FromBody] Pagination pagination)
        {
            Result result = await _userService.GetAllUserDetails(pagination);
            return Ok(result.ApiResult);
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto userModel)
        {
            Result result = await _userService.DeleteUser(userModel);
            return Ok(result.ApiResult);
        }
    }
}
