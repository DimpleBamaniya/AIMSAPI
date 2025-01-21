using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IUserProductRepository
    {
        Task<IEnumerable<UserProducts>> GetAllUserProduct();
        Task<List<UserProducts>> GetAllUserProductAsync();
        Task<bool> DeleteUserProductAsync(int id);
        Task<UserProducts> SaveUserProducts(UserProducts userProducts);
        Task<List<ProductByUserID>> GetProductListbyUserID(int id);
        Task<CheckUserProductCategoryMatchDto> CheckUserProductCategoryMatchAsync(int userId, int categoryId);
    }
}
