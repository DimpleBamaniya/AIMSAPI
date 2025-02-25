using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("GetUserDetailByEmailID")]
    public async Task<IActionResult> GetUserDetailByEmailID([FromBody] LoginDto loginRequest)
    {
        Result result = await _userService.GetUserDetailByEmailIDAsync(loginRequest);
        return Ok(result.ApiResult);
    }

    [HttpPost("GetUserDetailByID")]
    [Authorize]
    public async Task<IActionResult> GetUserDetailByID([FromBody] UserIdRequest userIdRequest)
    {
        Result result = await _userService.GetUserDetailByIDAsync(userIdRequest.ID);
        return Ok(result.ApiResult);
    }

    [HttpPost("SaveUser")]
    [Authorize]
    public async Task<IActionResult> SaveUser(UserDto userModel)
    {
        Result result = await _userService.SaveUserAsync(userModel);
        return Ok(result.ApiResult);
    }

    [HttpPost("GetAllUserDetails")]
    [Authorize]
    public async Task<IActionResult> GetAllUserDetails([FromBody] Pagination pagination)
    {
        Result result = await _userService.GetAllUserDetailsAsync(pagination);
        return Ok(result.ApiResult);
    }

    [HttpPost("DeleteUser")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(DeleteUserDto userModel)
    {
        Result result = await _userService.DeleteUserAsync(userModel);
        return Ok(result.ApiResult);
    }
}
