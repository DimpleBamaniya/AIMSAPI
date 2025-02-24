namespace Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Departments>> GetAllDepartmentsAsync()
    {
        return await _context.Departments.AsNoTracking()
            .Where(d => d.IsActive)
            .Select(d => new Departments { ID = d.ID, Name = d.Name })
            .ToListAsync();
    }
}