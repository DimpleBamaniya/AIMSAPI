using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
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

        public async Task<Result> GetProductListbyUserID(int id)
        {
            try
            {
                List<ProductByUserID> productList = (await _userProductRepository.GetProductListbyUserID(id)).ToList();
                return new Result { ResultObject = productList };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while fetching all user list by product id, ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
