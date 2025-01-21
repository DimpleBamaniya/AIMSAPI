using System.Collections.Generic;
using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace AIMSV2.Services
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UserProductService(IUserProductRepository userProductRepository, IMapper mapper, IProductService productService)
        {
            _userProductRepository = userProductRepository;
            _mapper = mapper;
            _productService = productService;
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

        public async Task<bool> DeleteUserProductAsync(int id)
        {
            var result = await _userProductRepository.DeleteUserProductAsync(id);
            await _productService.ExecuteUpdateAvailableQuantityAndUseQuantity();
            return result;
        }

        public async Task<Result> SaveUserProducts(SaveUserProduct userProductModel)
        {
            try
            {

                UserProducts? userProduct = null;
                SaveProductDto productModel = null;
                #region API Validations
                Result validationResult = await ValiadationFields(userProductModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return validationResult;
                }
                #endregion

                List<int> productID = await _productService.GetProductIdsByCategoryAndBrandAsync(userProductModel.CategoryID, userProductModel.BrandID);
                if (productID.Count == 0)
                {
                    productModel = new SaveProductDto
                    {
                        ID = 0,
                        CategoryID = userProductModel.CategoryID,
                        BrandID = userProductModel.BrandID,
                        Quantity = null,
                        CreatedBy = userProductModel.CreatedBy
                    };
                    var result = await _productService.SaveProduct(productModel);
                    productID = await _productService.GetProductIdsByCategoryAndBrandAsync(userProductModel.CategoryID, userProductModel.BrandID);
                }
                List<ProductByUserID> productByUserID = await _userProductRepository.GetProductListbyUserID(userProductModel.ID);
                bool productExists = productByUserID.Any(product => product.ID == productID[0]);

                var userProductMatch = await _userProductRepository.CheckUserProductCategoryMatchAsync(userProductModel.ID, userProductModel.CategoryID);

                if (productExists)
                {
                    return new Result($"Product is Already Assigned", "UserProducts.AssignedProduct", HttpStatusCode.BadRequest);
                }
                else if (userProductMatch.IsMatch)
                {
                    return new Result($"Product category is Already Assigned", "UserProducts.AssignedProductCategory", HttpStatusCode.BadRequest);
                }
                else
                {
                    userProduct = new UserProducts
                    {
                        UserID = userProductModel.ID,
                        ProductID = productID[0]
                    };
                    userProduct = await _userProductRepository.SaveUserProducts(userProduct);
                    await _productService.ExecuteUpdateAvailableQuantityAndUseQuantity();
                    return new Result { ResultObject = userProduct };
                }

            }
            catch (Exception ex)
            {
                return new Result("An error occurred while save user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task<Result> ValiadationFields(SaveUserProduct userProductModel)
        {
            if (userProductModel == null)
            {
                return new Result($"Product Model is null", "UserProducts.ProductModelRequired", HttpStatusCode.BadRequest);
            }
            if (userProductModel.ID == null)
            {
                return new Result($"UserID is mandatory.", "UserProducts.UserIDIsMandatory", HttpStatusCode.BadRequest);
            }
            if (userProductModel.CategoryID == null)
            {
                return new Result($"CategoryID is mandatory.", "UserProducts.CategoryIDIsMandatory", HttpStatusCode.BadRequest);
            }
            if (userProductModel.BrandID == null)
            {
                return new Result($"BrandID is mandatory.", "UserProducts.BrandIDIsMandatory", HttpStatusCode.BadRequest);
            }

            return new Result();
        }

        public async Task<Result> GetProductListByUserId(int id)
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

        public async Task<Result> CheckUserProductCategoryMatchAsync(int userId, int categoryId)
        {
            try
            {
                // Get the result from the repository
                var result = await _userProductRepository.CheckUserProductCategoryMatchAsync(userId, categoryId);

                // Check if result is not null and return appropriate result
                if (result != null)
                {
                    var isMatch = result.IsMatch == true; // True if match, false otherwise
                    return new Result
                    {
                        Status = StatusType.Success,
                        HttpStatusCode = HttpStatusCode.OK,
                        Message = isMatch ? "Match found" : "No match found",
                        ResultObject = isMatch // Pass the result (true/false) to ResultObject
                    };
                }

                // Return failure result if no match found
                return new Result("No result found for the provided UserID and CategoryID.", "Products.NoMatchFound", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while checking the user product category match. ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
