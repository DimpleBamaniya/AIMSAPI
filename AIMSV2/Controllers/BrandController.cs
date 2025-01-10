using AIMSV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIMSV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("GetActiveBrands")]
        public IActionResult GetActiveBrands()
        {
            var brands = _brandService.GetActiveBrands();
            if (!brands.Any())
            {
                return NotFound("No active brands found.");
            }
            return Ok(brands);
        }
    }
}
