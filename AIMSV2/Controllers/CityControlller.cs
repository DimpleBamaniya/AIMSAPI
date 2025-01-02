using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AIMSV2.Models;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        [HttpPost("GetAllCities")]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCities();
            return Ok(cities);
        }
    } 
}
