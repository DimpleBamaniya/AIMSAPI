using AIMSV2.Repositories;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CityService> _logger;

        public CityService(ICityRepository cityRepository, IMapper mapper, ILogger<CityService> logger)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _logger = logger;
        }

        public async Task<IEnumerable<Cities>> GetAllCities()
        {
            _logger.LogInformation("Started GetAllCities()");

            _logger.LogDebug("Started GetAllCities()");
            var result  = await _cityRepository.GetAllCities();
            _logger.LogDebug("Completed GetAllCities()" + result);

            _logger.LogInformation("Completed GetAllCities()");
            return result;
        }
    }
}
