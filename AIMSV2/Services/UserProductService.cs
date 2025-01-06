using AIMSV2.Repositories;
using AIMSV2.Repositories;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly IMapper _mapper;

        public UserProductService(IUserProductRepository userProductRepository, IMapper mapper)
        {
            _userProductRepository = userProductRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserProducts>> GetAllUserProduct()
        {
            return await _userProductRepository.GetAllUserProduct();
        }

        public async Task<List<UserProducts>> GetAllUserProductAsync()
        {
            return await _userProductRepository.GetAllUserProductAsync();
        }

    }
}
