using AIMSV2.Models;
using AIMSV2.Repositories;
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
    }
}
