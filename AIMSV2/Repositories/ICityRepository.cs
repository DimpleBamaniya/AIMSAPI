using Entities;

namespace AIMSV2.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<Cities>> GetAllCities();
    }
}
