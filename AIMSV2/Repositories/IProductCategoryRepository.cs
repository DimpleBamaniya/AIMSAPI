using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategoryDto> GetActiveProductCategories();
        //Task<Products> GetProductDetailByID(int id);
        //Task<List<ProductDetails>> GetAllProductDetails(Pagination pagination);
    }
}
