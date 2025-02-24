namespace Repositories;
public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategoryDto>> GetActiveProductCategoriesAsync();
}
