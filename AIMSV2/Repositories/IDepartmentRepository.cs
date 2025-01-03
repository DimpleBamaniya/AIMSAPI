namespace AIMSV2.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Entities.Departments>> GetAllDepartments();
    }
}
