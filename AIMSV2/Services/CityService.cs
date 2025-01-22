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
            try
            {
                _logger.LogInformation("Started GetAllCities()");

                _logger.LogDebug("Fetching all cities from the repository.");
                var result = await _cityRepository.GetAllCities();
                _logger.LogDebug("Successfully fetched all cities: {@Result}", result);

                _logger.LogInformation("Completed GetAllCities()");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all cities.");
                // Optionally, rethrow the exception or handle it as needed.
                throw;
            }
        }
    }
}
