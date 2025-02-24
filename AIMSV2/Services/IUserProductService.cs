namespace Services;
public interface IUserProductService
{
    Task<Result> GetAllUserProductAsync();
    Task<Result> DeleteUserProductAsync(int id);
    Task<Result> SaveUserProductsAsync(SaveUserProduct userProductModel);
    Task<Result> GetProductListbyUserIDAsync(int id);
    Task<Result> CheckUserProductCategoryMatchAsync(int userId, int categoryId);
}
