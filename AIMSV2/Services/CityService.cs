using Entities;

namespace Services;
public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<CityService> _logger;

    public CityService(ICityRepository cityRepository, IMapper mapper, ILogger<CityService> logger)
    {
        _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        _logger = logger;
    }

    public async Task<Result> GetAllCitiesAsync()
    {
        _logger.LogInformation("Started GetAllCitiesAsync()");
        try
        {
            _logger.LogDebug("Fetching all cities from the repository.");
            var cities = await _cityRepository.GetAllCitiesAsync();
            _logger.LogDebug("Successfully fetched all cities: {@Result}", cities);

            _logger.LogInformation("Completed GetAllCitiesAsync()");

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Active cities retrieved successfully.",
                ResultObject = cities
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active cities.");
            return new Result("An error occurred while retrieving active cities.", "ERROR_FETCHING_CITIES", HttpStatusCode.InternalServerError, ex);
        }
    }

}
