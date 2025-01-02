using AIMSV2.Data;
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

        public async Task<IEnumerable<Entities.Cities>> GetAllCities()
        {
            return await _context.Cities.ToListAsync();
        }
    }
}