using AIMSV2.Repositories;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        }

        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            return await _departmentRepository.GetAllDepartments();
        }
    }
}
