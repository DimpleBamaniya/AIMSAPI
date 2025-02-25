using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;
    public CityController(ICityService cityService)
    {
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
    }

    [HttpPost("GetAllCities")]
    [Authorize]
    public async Task<IActionResult> GetAllCities()
    {
        Result result = await _cityService.GetAllCitiesAsync();
        return Ok(result.ApiResult);
    }
} 
