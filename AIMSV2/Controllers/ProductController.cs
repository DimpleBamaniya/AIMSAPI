using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
    } 
}
