namespace Repositories;
public interface IDepartmentRepository
{
    Task<IEnumerable<Departments>> GetAllDepartmentsAsync();
}
