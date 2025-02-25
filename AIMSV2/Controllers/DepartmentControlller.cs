using Microsoft.AspNetCore.Authorization;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
    }

    [HttpPost("GetAllDepartments")]
    [Authorize]
    public async Task<IActionResult> GetAllDepartments()
    {
        Result result = await _departmentService.GetAllDepartmentsAsync();
        return Ok(result.ApiResult);
    }
} 
