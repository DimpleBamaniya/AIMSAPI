using AIMSV2.Utility;

namespace AIMSV2.Services
{
    public interface IProductService
    {
        Task<bool> ExecuteUpdateUseQuantity();
        Task<bool> ExecuteUpdateAvailableQuantity();
        Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity();
        Task<Result> GetProductDetailByID(int id);
        Task<Result> GetAllProductDetails(Pagination pagination);
    }
}
