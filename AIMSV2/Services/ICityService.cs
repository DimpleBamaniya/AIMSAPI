using Entities;

namespace AIMSV2.Services
{
    public interface ICityService
    {
        Task<IEnumerable<Cities>> GetAllCities();
    }
}
