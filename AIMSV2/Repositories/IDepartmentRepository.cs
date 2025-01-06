using Entities;

namespace AIMSV2.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Departments>> GetAllDepartments();
    }
}
