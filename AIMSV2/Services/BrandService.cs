using System.Net;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;

namespace AIMSV2.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public IEnumerable<BrandDto> GetActiveBrands()
        {
            try
            {
                _logger.LogInformation("Started GetActiveBrands()");

                _logger.LogDebug("Fetching active brands from the repository.");
                var result = _brandRepository.GetActiveBrands();

                _logger.LogDebug("Successfully fetched active brands: {@Result}", result);
                _logger.LogInformation("Completed GetActiveBrands()");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching active brands.");
                // Optionally, rethrow the exception or handle it as needed.
                throw;
            }
        }

        public async Task<Result> GetBrandsByCategoryID(int categoryID)
        {
            try
            {
                _logger.LogInformation("Started GetBrandsByCategoryID()");

                _logger.LogDebug("Started GetBrandsByCategoryID()");
                List<BrandDto> result = (await _brandRepository.GetBrandsByCategoryID(categoryID)).ToList();
                _logger.LogDebug("Completed GetBrandsByCategoryID()");

                _logger.LogInformation("Completed  GetBrandsByCategoryID()");
                return new Result { ResultObject = result };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching all accounts", ex);
                return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
