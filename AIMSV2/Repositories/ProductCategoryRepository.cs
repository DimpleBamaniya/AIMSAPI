namespace Repositories;
public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public ProductCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductCategoryDto>> GetActiveProductCategoriesAsync()
    {
        return await _context.Categories.AsNoTracking()
            .Where(c => c.IsActive)
            .Select(c => new ProductCategoryDto
            {
                Id = c.ID,
                Name = c.Name
            })
            .ToListAsync();
    }

}