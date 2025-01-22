using AIMSV2.Models;
using AIMSV2.Repositories;
using AutoMapper;

namespace AIMSV2.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ILogger<ProductCategoryService> logger)
        {
            _productCategoryRepository = productCategoryRepository;
            _logger = logger;
        }

        public IEnumerable<ProductCategoryDto> GetActiveProductCategories()
        {
            try
            {
                _logger.LogInformation("Started GetActiveProductCategories()");

                _logger.LogDebug("Fetching active product categories from the repository.");
                var result = _productCategoryRepository.GetActiveProductCategories();
                _logger.LogDebug("Successfully fetched active product categories: {@Result}", result);

                _logger.LogInformation("Completed GetActiveProductCategories()");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching active product categories.");
                // Optionally, rethrow the exception or handle it as needed.
                throw;
            }
        }

    }
}
