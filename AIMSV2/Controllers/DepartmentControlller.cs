using Microsoft.AspNetCore.Mvc;
using AIMSV2.Data;
using AIMSV2.Services;
using AIMSV2.Models;

namespace AIMSV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [HttpPost("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var cities = await _departmentService.GetAllDepartments();
            return Ok(cities);
        }
    } 
}
