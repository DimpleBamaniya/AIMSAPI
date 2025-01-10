using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public IEnumerable<BrandDto> GetActiveBrands()
        {
            return _brandRepository.GetActiveBrands();
        }

        //public async Task<Result> GetProductDetailByID(int id)
        //{
        //    try
        //    {
        //        #region API Validations
        //        Result validationResult = await ValiadationFieldsToGetProductDetailByID(id);
        //        if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            return validationResult;
        //        }
        //        #endregion

        //        Products product = await _productCategoryRepository.GetProductDetailByID(id);
        //        if (product == null)
        //        {
        //            return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
        //        }
        //        else
        //        {
        //            return new Result { ResultObject = product };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
        //    }

        //}

        //private async Task<Result> ValiadationFieldsToGetProductDetailByID(int id)
        //{
        //    if (id == null)
        //    {
        //        return new Result($"ID is null", "Accounts.IDIsRequired", HttpStatusCode.BadRequest);
        //    }
        //    return new Result();
        //}

        //public async Task<Result> GetAllProductDetails(Pagination pagination)
        //{
        //    try
        //    {
        //        List<ProductDetails> userData = (await _productCategoryRepository.GetAllProductDetails(pagination)).ToList();
        //        return new Result { ResultObject = userData };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
        //    }
        //}


    }
}
