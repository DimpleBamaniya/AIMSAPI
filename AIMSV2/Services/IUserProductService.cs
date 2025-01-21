using Entities;
using AIMSV2.Utility;
using AIMSV2.Models;

namespace AIMSV2.Services
{
    public interface IUserProductService
    {
        Task<IEnumerable<UserProducts>> GetAllUserProduct();
        Task<List<UserProducts>> GetAllUserProductAsync();
        Task<Result> GetProductListbyUserID(int id);
        Task<bool> DeleteUserProductAsync(int id);
        Task<Result> SaveUserProducts(SaveUserProduct userProductModel);
        Task<Result> GetProductListByUserId(int id);
        Task<Result> CheckUserProductCategoryMatchAsync(int userId, int categoryId);
    }
}
