using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentService> logger)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _logger = logger;
        }

        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            try
            {
                _logger.LogInformation("Started GetAllDepartments()");

                _logger.LogDebug("Fetching all departments from the repository.");
                var result = await _departmentRepository.GetAllDepartments();
                _logger.LogDebug("Successfully fetched all departments: {@Result}", result);

                _logger.LogInformation("Completed GetAllDepartments()");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all departments.");
                // Optionally, rethrow the exception or handle it as needed.
                throw;
            }
        }
    }
}
