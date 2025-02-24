namespace Services;
public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly ILogger<BrandService> _logger;

    public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
    {
        _brandRepository = brandRepository;
        _logger = logger;
    }


    public async Task<Result> GetActiveBrandsAsync()
    {
        _logger.LogInformation("Started GetActiveBrandsAsync()");

        try
        {
            _logger.LogDebug("Fetching active brands from the repository.");
            var brands = await _brandRepository.GetActiveBrandsAsync(); // Await async method

            _logger.LogDebug("Successfully fetched active brands: {@Result}", brands);
            _logger.LogInformation("Completed GetActiveBrandsAsync()");

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Active brands retrieved successfully.",
                ResultObject = brands
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active brands.");
            return new Result("An error occurred while retrieving active brands.", "ERROR_FETCHING_BRANDS", HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<Result> GetBrandsByCategoryID(int categoryID)
    {
        _logger.LogInformation("Started GetBrandsByCategoryID()");

        try
        {
            _logger.LogDebug("Fetching brands for CategoryID: {CategoryID}", categoryID);
            List<BrandDto> brands = (await _brandRepository.GetBrandsByCategoryIDAsync(categoryID)).ToList();
            _logger.LogDebug("Completed fetching brands for CategoryID: {CategoryID}", categoryID);

            _logger.LogInformation("Completed GetBrandsByCategoryID()");

            if (brands == null || !brands.Any())
            {
                return new Result("No brands found for the given category.", "NO_BRANDS_FOUND", HttpStatusCode.NotFound);
            }

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Brands retrieved successfully.",
                ResultObject = brands
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching brands for CategoryID: {CategoryID}", categoryID);
            return new Result("An error occurred while fetching brands.", "BRANDS_FETCH_ERROR", HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<Result> GetUnassignedBrandsByCategory(int categoryID)
    {
        _logger.LogInformation("Started GetUnassignedBrandsByCategory()");

        try
        {
            _logger.LogDebug("Fetching brands for CategoryID: {CategoryID}", categoryID);
            List<BrandDto> brands = (await _brandRepository.GetUnassignedBrandsByCategoryAsync(categoryID)).ToList();
            _logger.LogDebug("Completed fetching brands for CategoryID: {CategoryID}", categoryID);

            _logger.LogInformation("Completed GetUnassignedBrandsByCategory()");

            if (brands == null || !brands.Any())
            {
                return new Result("All brands already added for the given category.", "NO_BRANDS_FOUND", HttpStatusCode.NotFound);
            }

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Brands retrieved successfully.",
                ResultObject = brands
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching brands for CategoryID: {CategoryID}", categoryID);
            return new Result("An error occurred while fetching brands.", "BRANDS_FETCH_ERROR", HttpStatusCode.InternalServerError, ex);
        }
    }

}
