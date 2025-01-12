using Entities;
using AIMSV2.Utility;

namespace AIMSV2.Services
{
    public interface IUserProductService
    {
        Task<IEnumerable<UserProducts>> GetAllUserProduct();
        Task<List<UserProducts>> GetAllUserProductAsync();
        Task<Result> GetProductListbyUserID(int id);
    }
}
