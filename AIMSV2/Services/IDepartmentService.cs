namespace Services;
public interface IDepartmentService
{
    Task<Result> GetAllDepartmentsAsync();
}
