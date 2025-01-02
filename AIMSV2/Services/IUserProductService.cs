using Entities;

namespace AIMSV2.Services
{
    public interface IUserProductService
    {
        Task<IEnumerable<Entities.UserProducts>> GetAllUserProduct();
        Task<List<UserProducts>> GetAllUserProductAsync();
    }
}
