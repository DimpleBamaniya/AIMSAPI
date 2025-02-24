namespace Repositories;
public class CityRepository : ICityRepository
{
    private readonly ApplicationDbContext _context;

    public CityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cities>> GetAllCitiesAsync()
    {
        return await _context.Cities.AsNoTracking()
            .Where(c => c.IsActive)
            .Select(c => new Cities { ID = c.ID, Name = c.Name })
            .ToListAsync();
    }
}