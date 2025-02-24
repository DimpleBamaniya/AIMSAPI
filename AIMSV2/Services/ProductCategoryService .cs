using Entities;

namespace Services;
public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ILogger<ProductCategoryService> _logger;

    public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ILogger<ProductCategoryService> logger)
    {
        _productCategoryRepository = productCategoryRepository;
        _logger = logger;
    }

    public async Task<Result> GetActiveProductCategoriesAsync()
    {
        _logger.LogInformation("Started GetActiveProductCategoriesAsync()");
        try
        {
            _logger.LogDebug("Fetching active product categories from the repository.");
            var productCategories = await _productCategoryRepository.GetActiveProductCategoriesAsync();
            _logger.LogDebug("Successfully fetched active product categories: {@Result}", productCategories);

            _logger.LogInformation("Completed GetActiveProductCategoriesAsync()");

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Active product categories retrieved successfully.",
                ResultObject = productCategories
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active product categories.");
            return new Result("An error occurred while retrieving active product categories.", "ERROR_FETCHING_CATEGORIES", HttpStatusCode.InternalServerError, ex);
        }
    }

}
