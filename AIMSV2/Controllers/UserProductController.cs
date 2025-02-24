using Microsoft.AspNetCore.Authorization;
using Models;

namespace Controllers;
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

    [HttpPost("GetProductListbyUserID")]
    [Authorize]
    public async Task<IActionResult> GetProductListbyUserID([FromBody] UserIdRequest userIdRequest)
    {
        Result result = await _userProductService.GetProductListbyUserIDAsync(userIdRequest.ID);
        return Ok(result.ApiResult);
    }

    [HttpPost("DeleteUserProduct")]
    [Authorize]
    public async Task<IActionResult> DeleteUserProduct([FromBody] UserProductIdRequest userProductIdRequest)
    {
        Result result = await _userProductService.DeleteUserProductAsync(userProductIdRequest.ID);
        return Ok(result.ApiResult);
    }

    [HttpPost("SaveUserProducts")]
    [Authorize]
    public async Task<IActionResult> SaveUserProducts([FromBody] SaveUserProduct userProductModel)
    {
        Result result = await _userProductService.SaveUserProductsAsync(userProductModel);
        return Ok(result.ApiResult);
    }
}

