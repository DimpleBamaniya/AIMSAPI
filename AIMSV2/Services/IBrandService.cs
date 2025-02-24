namespace Services;
public interface IBrandService
{
    Task<Result> GetActiveBrandsAsync();
    Task<Result> GetBrandsByCategoryID(int categoryID);
    Task<Result> GetUnassignedBrandsByCategory(int categoryID);
}
