using AIMSV2.Models;
using AIMSV2.Utility;

namespace AIMSV2.Services
{
    public interface IBrandService
    {
        IEnumerable<BrandDto> GetActiveBrands();
        Task<Result> GetBrandsByCategoryID(int categoryID);
    }
}
