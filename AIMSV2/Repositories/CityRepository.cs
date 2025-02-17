using AIMSV2.Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cities>> GetAllCities()
        {
            return await _context.Cities.AsNoTracking().ToListAsync();
        }
    }
}