using System.Net;
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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserProductService> _logger;

        public UserProductService(IUserProductRepository userProductRepository, IMapper mapper, IProductService productService, ILogger<UserProductService> logger)
        {
            _userProductRepository = userProductRepository;
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
        }

        public async Task<IEnumerable<UserProducts>> GetAllUserProduct()
        {
            try
            {
                _logger.LogInformation("Started GetAllUserProduct");

                _logger.LogDebug("Calling repository method to fetch all user products.");
                IEnumerable<UserProducts> userProducts = await _userProductRepository.GetAllUserProduct();
                _logger.LogDebug("Successfully fetched {UserProductsCount} user products.", userProducts.Count());

                _logger.LogInformation("Completed GetAllUserProduct");

                return userProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all user products.");
                throw; // Re-throw the exception to allow higher-level handling
            }
        }

        public async Task<List<UserProducts>> GetAllUserProductAsync()
        {
            try
            {
                _logger.LogInformation("Started GetAllUserProductAsync");

                _logger.LogDebug("Calling repository method to fetch all user products asynchronously.");
                List<UserProducts> userProducts = await _userProductRepository.GetAllUserProductAsync();

                _logger.LogDebug("Successfully fetched {UserProductsCount} user products asynchronously.", userProducts.Count);
                _logger.LogInformation("Completed GetAllUserProductAsync");

                return userProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all user products asynchronously.");
                throw; // Re-throw the exception to propagate it for higher-level handling
            }
        }


        public async Task<Result> GetProductListbyUserID(int id)
        {
            try
            {
                _logger.LogInformation("Started GetProductListbyUserID with UserID: {UserID}", id);

                _logger.LogDebug("Fetching product list for UserID: {UserID}", id);
                List<ProductByUserID> productList = (await _userProductRepository.GetProductListbyUserID(id)).ToList();

                _logger.LogDebug("Successfully fetched {ProductListCount} products for UserID: {UserID}", productList.Count, id);
                _logger.LogInformation("Completed GetProductListbyUserID for UserID: {UserID}", id);

                return new Result { ResultObject = productList };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching product list for UserID: {UserID}", id);
                return new Result("An error occurred while fetching product list by user ID, ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<bool> DeleteUserProductAsync(int id)
        {
            try
            {
                _logger.LogInformation("Started DeleteUserProductAsync with UserProductID: {UserProductID}", id);

                _logger.LogDebug("Calling repository method to delete user product with UserProductID: {UserProductID}", id);
                bool result = await _userProductRepository.DeleteUserProductAsync(id);

                if (result)
                {
                    _logger.LogDebug("Successfully deleted user product with UserProductID: {UserProductID}", id);
                    _logger.LogDebug("Calling ExecuteUpdateAvailableQuantityAndUseQuantity to update product quantities.");
                    await _productService.ExecuteUpdateAvailableQuantityAndUseQuantity();
                    _logger.LogDebug("Successfully updated available quantity and use quantity.");
                }
                else
                {
                    _logger.LogWarning("Failed to delete user product with UserProductID: {UserProductID}", id);
                }

                _logger.LogInformation("Completed DeleteUserProductAsync with UserProductID: {UserProductID}", id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user product with UserProductID: {UserProductID}", id);
                return false;
            }
        }

        public async Task<Result> SaveUserProducts(SaveUserProduct userProductModel)
        {
            try
            {
                _logger.LogInformation("Started SaveUserProducts with UserID: {UserID}", userProductModel.ID);

                _logger.LogDebug("Validating fields for SaveUserProduct with UserID: {UserID}", userProductModel.ID);
                Result validationResult = await ValiadationFields(userProductModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    _logger.LogWarning("Validation failed for SaveUserProduct with UserID: {UserID}", userProductModel.ID);
                    return validationResult;
                }

                _logger.LogDebug("Fetching product IDs for CategoryID: {CategoryID}, BrandID: {BrandID}", userProductModel.CategoryID, userProductModel.BrandID);
                List<int> productID = await _productService.GetProductIdsByCategoryAndBrandAsync(userProductModel.CategoryID, userProductModel.BrandID);

                if (productID.Count == 0)
                {
                    _logger.LogDebug("No products found for CategoryID: {CategoryID}, BrandID: {BrandID}. Creating new product.", userProductModel.CategoryID, userProductModel.BrandID);
                    SaveProductDto productModel = new SaveProductDto
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

                _logger.LogDebug("Checking if the product is already assigned to the user with UserID: {UserID}", userProductModel.ID);
                List<ProductByUserID> productByUserID = await _userProductRepository.GetProductListbyUserID(userProductModel.ID);
                bool productExists = productByUserID.Any(product => product.ID == productID[0]);

                _logger.LogDebug("Checking if the product category is already assigned to the user with UserID: {UserID}", userProductModel.ID);
                var userProductMatch = await _userProductRepository.CheckUserProductCategoryMatchAsync(userProductModel.ID, userProductModel.CategoryID);

                if (productExists)
                {
                    _logger.LogWarning("Product is already assigned to user with UserID: {UserID}", userProductModel.ID);
                    return new Result($"Product is Already Assigned", "UserProducts.AssignedProduct", HttpStatusCode.BadRequest);
                }
                else if (userProductMatch.IsMatch)
                {
                    _logger.LogWarning("Product category is already assigned to user with UserID: {UserID}", userProductModel.ID);
                    return new Result($"Product category is Already Assigned", "UserProducts.AssignedProductCategory", HttpStatusCode.BadRequest);
                }
                else
                {
                    _logger.LogDebug("Assigning product to user with UserID: {UserID}", userProductModel.ID);
                    UserProducts userProduct = new UserProducts
                    {
                        UserID = userProductModel.ID,
                        ProductID = productID[0]
                    };
                    userProduct = await _userProductRepository.SaveUserProducts(userProduct);

                    _logger.LogDebug("Product assigned successfully. Updating available quantity and use quantity.");
                    await _productService.ExecuteUpdateAvailableQuantityAndUseQuantity();

                    _logger.LogInformation("Successfully saved user product with UserID: {UserID}", userProductModel.ID);
                    return new Result { ResultObject = userProduct };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving user product with UserID: {UserID}", userProductModel.ID);
                return new Result("An error occurred while saving user product", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }


        private async Task<Result> ValiadationFields(SaveUserProduct userProductModel)
        {
            try
            {
                _logger.LogInformation("Started validation for SaveUserProduct with UserID: {UserID}", userProductModel?.ID);

                if (userProductModel == null)
                {
                    _logger.LogWarning("Validation failed. Product model is null.");
                    return new Result($"Product Model is null", "UserProducts.ProductModelRequired", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validating UserID: {UserID}", userProductModel.ID);
                if (userProductModel.ID == null)
                {
                    _logger.LogWarning("Validation failed. UserID is mandatory.");
                    return new Result($"UserID is mandatory.", "UserProducts.UserIDIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validating CategoryID: {CategoryID}", userProductModel.CategoryID);
                if (userProductModel.CategoryID == null)
                {
                    _logger.LogWarning("Validation failed. CategoryID is mandatory.");
                    return new Result($"CategoryID is mandatory.", "UserProducts.CategoryIDIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validating BrandID: {BrandID}", userProductModel.BrandID);
                if (userProductModel.BrandID == null)
                {
                    _logger.LogWarning("Validation failed. BrandID is mandatory.");
                    return new Result($"BrandID is mandatory.", "UserProducts.BrandIDIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogInformation("Validation successful for SaveUserProduct with UserID: {UserID}", userProductModel.ID);
                return new Result();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during validation for SaveUserProduct with UserID: {UserID}", userProductModel?.ID);
                return new Result("An error occurred while validating the user product.", "UserProducts.ValidationError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> GetProductListByUserId(int id)
        {
            try
            {
                _logger.LogInformation("Started fetching product list for UserID: {UserID}", id);

                // Log debug information before calling the repository
                _logger.LogDebug("Calling repository method to get product list for UserID: {UserID}", id);

                List<ProductByUserID> productList = (await _userProductRepository.GetProductListbyUserID(id)).ToList();

                _logger.LogDebug("Fetched {ProductCount} products for UserID: {UserID}", productList.Count, id);

                _logger.LogInformation("Completed fetching product list for UserID: {UserID}", id);

                return new Result { ResultObject = productList };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the product list for UserID: {UserID}", id);
                return new Result("An error occurred while fetching all user list by product id, ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> CheckUserProductCategoryMatchAsync(int userId, int categoryId)
        {
            try
            {
                _logger.LogInformation("Started checking user product category match for UserID: {UserID} and CategoryID: {CategoryID}", userId, categoryId);

                // Log debug before calling repository
                _logger.LogDebug("Calling repository method to check category match for UserID: {UserID} and CategoryID: {CategoryID}", userId, categoryId);

                var result = await _userProductRepository.CheckUserProductCategoryMatchAsync(userId, categoryId);

                if (result != null)
                {
                    var isMatch = result.IsMatch == true; // True if match, false otherwise

                    // Log the match result
                    _logger.LogDebug("Match result for UserID: {UserID} and CategoryID: {CategoryID} - Match: {IsMatch}", userId, categoryId, isMatch);

                    return new Result
                    {
                        Status = StatusType.Success,
                        HttpStatusCode = HttpStatusCode.OK,
                        Message = isMatch ? "Match found" : "No match found",
                        ResultObject = isMatch // Pass the result (true/false) to ResultObject
                    };
                }

                // Log when no result is found
                _logger.LogWarning("No match found for UserID: {UserID} and CategoryID: {CategoryID}", userId, categoryId);

                // Return failure result if no match found
                return new Result("No result found for the provided UserID and CategoryID.", "Products.NoMatchFound", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while checking the user product category match for UserID: {UserID} and CategoryID: {CategoryID}", userId, categoryId);

                return new Result("An error occurred while checking the user product category match. ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
