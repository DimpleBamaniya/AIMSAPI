using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IProductRepository
    {
        Task ExecuteUpdateUseQuantity();
        Task ExecuteUpdateAvailableQuantity();
        Task ExecuteUpdateAvailableQuantityAndUseQuantity();
        Task<Products> GetProductDetailByID(int id);
        Task<List<ProductDetails>> GetAllProductDetails(Pagination pagination);
    }
}
