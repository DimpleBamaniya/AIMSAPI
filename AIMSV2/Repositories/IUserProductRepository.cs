using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IUserProductRepository
    {
        Task<IEnumerable<Entities.UserProducts>> GetAllUserProduct();
        Task<List<UserProducts>> GetAllUserProductAsync();
    }
}
