using AIMSV2.Models;
using AIMSV2.Utility;

namespace AIMSV2.Services
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategoryDto> GetActiveProductCategories();
        //Task<Result> GetProductDetailByID(int id);
        //Task<Result> GetAllProductDetails(Pagination pagination);
    }
}
