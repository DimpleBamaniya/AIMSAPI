using AIMSV2.Repositories;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        public async Task<IEnumerable<Cities>> GetAllCities()
        {
            return await _cityRepository.GetAllCities();
        }
    }
}
