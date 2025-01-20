using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IBrandRepository
    {
        IEnumerable<BrandDto> GetActiveBrands();
        Task<List<BrandDto>> GetBrandsByCategoryID(int categoryID);
    }
}
