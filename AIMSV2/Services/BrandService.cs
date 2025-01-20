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
        private readonly IMapper _mapper;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<BrandDto> GetActiveBrands()
        {
            _logger.LogInformation("Started GetActiveBrands()");

            _logger.LogDebug("Started GetActiveBrands()");
            var result = _brandRepository.GetActiveBrands();
            _logger.LogDebug("Completed GetActiveBrands()" + result);

            _logger.LogInformation("Completed GetActiveBrands()");

            return result;
        }


        public async Task<Result> GetBrandsByCategoryID(int categoryID)
        {
            try
            {
                List<BrandDto> result = (await _brandRepository.GetBrandsByCategoryID(categoryID)).ToList();
                return new Result { ResultObject = result };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
