namespace Repositories;
public interface IBrandRepository
{
    Task<IEnumerable<BrandDto>> GetActiveBrandsAsync();
    Task<List<BrandDto>> GetBrandsByCategoryIDAsync(int categoryID);
    Task<List<BrandDto>> GetUnassignedBrandsByCategoryAsync(int categoryID);
}
