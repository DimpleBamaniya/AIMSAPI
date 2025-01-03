using Entities;

namespace AIMSV2.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Entities.Departments>> GetAllDepartments();
    }
}
