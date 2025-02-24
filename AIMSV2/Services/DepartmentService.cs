using Entities;

namespace Services;
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentService> logger)
    {
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        _logger = logger;
    }

    public async Task<Result> GetAllDepartmentsAsync()
    {
        _logger.LogInformation("Started GetAllDepartments()");
        try
        {

            _logger.LogDebug("Fetching all departments from the repository.");
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            _logger.LogDebug("Successfully fetched all departments: {@Result}", departments);

            _logger.LogInformation("Completed GetAllDepartments()");

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Active departments retrieved successfully.",
                ResultObject = departments
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active departments.");
            return new Result("An error occurred while retrieving active departments.", "ERROR_FETCHING_DEPARTMENTS", HttpStatusCode.InternalServerError, ex);
        }
    }
}
