using AIMSV2.Utility;
using AIMSV2.Models;

namespace AIMSV2.Services
{
    public interface IProductService
    {
        Task<bool> ExecuteUpdateUseQuantity();
        Task<bool> ExecuteUpdateAvailableQuantity();
        Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity();
        Task<Result> GetProductDetailByID(int id);
        Task<Result> GetAllProductDetails(Pagination pagination);
        Task<Result> SaveProduct(SaveProductDto productModel);
        Task<bool> CheckIfProductExistsAsync(int categoryId,int brandId);
        Task<bool> ExecuteUpdateProductCodes();
        Task<Result> GetUserListByProductID(int id);
        Task<Result> DeleteProduct(DeleteProductDto productModel);
    }
}
