namespace Services;
public interface IProductCategoryService
{
    Task<Result> GetActiveProductCategoriesAsync();
}
