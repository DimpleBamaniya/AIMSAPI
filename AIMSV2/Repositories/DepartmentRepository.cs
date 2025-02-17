using AIMSV2.Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }
    }
}