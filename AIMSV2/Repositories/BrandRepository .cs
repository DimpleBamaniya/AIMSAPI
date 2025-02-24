namespace Repositories;
public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BrandDto>> GetActiveBrandsAsync()
    {
        return await _context.Brands.AsNoTracking()
            .Where(b => b.IsActive)
            .Select(b => new BrandDto { Id = b.ID, Name = b.Name })
            .ToListAsync();
    }

    public async Task<List<BrandDto>> GetBrandsByCategoryIDAsync(int categoryID)
    {
        return await _context.usp_GetBrandsByCategoryAndAvailability
            .FromSqlRaw("Exec usp_GetBrandsByCategoryAndAvailability @CategoryID={0}",
                categoryID)
            .ToListAsync();
    }

    public async Task<List<BrandDto>> GetUnassignedBrandsByCategoryAsync(int categoryID)
    {
        return await _context.usp_GetUnassignedBrandsByCategory
            .FromSqlRaw("Exec usp_GetUnassignedBrandsByCategory @CategoryID={0}",
                categoryID)
            .ToListAsync();
    }

}