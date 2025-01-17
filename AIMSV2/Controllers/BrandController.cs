using AIMSV2.Services;
using AIMSV2.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

            try
            {
                var brands = _brandService.GetActiveBrands();
                if (!brands.Any())
                {
                    return NotFound("No active brands found.");
                }
                return Ok(brands);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
