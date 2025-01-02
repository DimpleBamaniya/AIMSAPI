using Entities;

namespace AIMSV2.Services
{
    public interface ICityService
    {
        Task<IEnumerable<Entities.Cities>> GetAllCities();
    }
}
