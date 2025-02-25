using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet("GetActiveBrands")]
    [Authorize]
    public async Task<IActionResult> GetActiveBrands()
    {
        Result result = await _brandService.GetActiveBrandsAsync();
        return Ok(result.ApiResult);
    }
    
    [HttpPost("GetBrandsByCategoryID")]
    [Authorize]
    public async Task<IActionResult> GetBrandsByCategoryID([FromBody] CategoryIdRequest categoryIdRequest)
    {
        Result result = await _brandService.GetBrandsByCategoryID(categoryIdRequest.ID);
        return Ok(result.ApiResult);
    }

    [HttpPost("GetUnassignedBrandsByCategory")]
    [Authorize]
    public async Task<IActionResult> GetUnassignedBrandsByCategory([FromBody] CategoryIdRequest categoryIdRequest)
    {
        Result result = await _brandService.GetUnassignedBrandsByCategory(categoryIdRequest.ID);
        return Ok(result.ApiResult);
    }
}
