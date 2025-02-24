namespace Repositories;
public interface IUserProductRepository
{
    Task<List<UserProducts>> GetAllUserProductAsync();
    Task<bool> DeleteUserProductAsync(int id);
    Task<UserProducts> SaveUserProductsAsync(UserProducts userProducts);
    Task<List<ProductByUserID>> GetProductListbyUserIDAsync(int id);
    Task<CheckUserProductCategoryMatchDto> CheckUserProductCategoryMatchAsync(int userId, int categoryId);
}
