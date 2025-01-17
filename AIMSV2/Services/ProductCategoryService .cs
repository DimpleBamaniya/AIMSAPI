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
            _logger.LogInformation("Started ()");

            _logger.LogDebug("Started GetActiveProductCategories()");
            var result = _productCategoryRepository.GetActiveProductCategories();
            _logger.LogDebug("Completed GetActiveProductCategories()" + result);

            _logger.LogInformation("Completed ()");
            return result;
        }

    }
}
