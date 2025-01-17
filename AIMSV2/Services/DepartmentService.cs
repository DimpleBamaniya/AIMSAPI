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

            _logger.LogInformation("Started GetAllDepartments()");

            _logger.LogDebug("Started GetAllDepartments()");
            var result = await _departmentRepository.GetAllDepartments();
            _logger.LogDebug("Completed GetAllDepartments()" + result);

            _logger.LogInformation("Completed GetAllDepartments()");
            return result;
        }
    }
}
