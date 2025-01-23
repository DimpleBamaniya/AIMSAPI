using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;

namespace AIMSV2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> ExecuteUpdateUseQuantity()
        {
            try
            {
                _logger.LogInformation("Started ExecuteUpdateUseQuantity()");

                _logger.LogDebug("Calling ExecuteUpdateUseQuantity() on the product repository.");
                await _productRepository.ExecuteUpdateUseQuantity();
                _logger.LogDebug("Successfully executed ExecuteUpdateUseQuantity() in the product repository.");

                _logger.LogInformation("Completed ExecuteUpdateUseQuantity()");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing ExecuteUpdateUseQuantity().");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantity()
        {
            try
            {
                _logger.LogInformation("Started ExecuteUpdateAvailableQuantity()");
                
                _logger.LogDebug("Calling ExecuteUpdateAvailableQuantity() on the product repository.");
                await _productRepository.ExecuteUpdateAvailableQuantity();
                _logger.LogDebug("Successfully executed ExecuteUpdateAvailableQuantity() in the product repository.");
                
                _logger.LogInformation("Completed ExecuteUpdateAvailableQuantity()");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing ExecuteUpdateAvailableQuantity().");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity()
        {
            try
            {
                _logger.LogInformation("Started ExecuteUpdateAvailableQuantityAndUseQuantity()");
                
                _logger.LogDebug("Calling ExecuteUpdateAvailableQuantityAndUseQuantity() on the product repository.");
                await _productRepository.ExecuteUpdateAvailableQuantityAndUseQuantity();
                _logger.LogDebug("Successfully executed ExecuteUpdateAvailableQuantityAndUseQuantity() in the product repository.");
                
                _logger.LogInformation("Completed ExecuteUpdateAvailableQuantityAndUseQuantity()");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing ExecuteUpdateAvailableQuantityAndUseQuantity().");
                return false;
            }
        }

        public async Task<Result> GetProductDetailByID(int id)
        {
            try
            {
                _logger.LogInformation("Started GetProductDetailByID with ID: {Id}", id);

                #region API Validations
                _logger.LogDebug("Validating fields for GetProductDetailByID with ID: {Id}", id);
                Result validationResult = await ValiadationFieldsToGetProductDetailByID(id);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    _logger.LogDebug("Validation failed for GetProductDetailByID with ID: {Id}. Error: {Error}", id, validationResult.ErrorMessage);
                    return validationResult;
                }
                #endregion

                _logger.LogDebug("Fetching product details for ID: {Id}", id);
                Products product = await _productRepository.GetProductDetailByID(id);

                if (product == null)
                {
                    _logger.LogDebug("Product not found for ID: {Id}", id);
                    return new Result($"Product is not Exists.", "Product.ProductNotExists", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Successfully fetched product details for ID: {Id}", id);

                _logger.LogInformation("Completed GetProductDetailByID with ID: {Id}", id);
                return new Result { ResultObject = product };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting product details for ID: {Id}", id);
                return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }
        }

        private async Task<Result> ValiadationFieldsToGetProductDetailByID(int id)
        {
            try
            {
                _logger.LogInformation("Started ValiadationFieldsToGetProductDetailByID with ID: {Id}", id);

                if (id == null)
                {
                    _logger.LogDebug("Validation failed: ID is null.");
                    return new Result($"ID is null", "Accounts.IDIsRequired", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validation succeeded for ID: {Id}", id);

                _logger.LogInformation("Completed ValiadationFieldsToGetProductDetailByID with ID: {Id}", id);
                return new Result();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during validation in ValiadationFieldsToGetProductDetailByID for ID: {Id}", id);
                return new Result($"{ex.Message}", "ValidationError", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> GetAllProductDetails(Pagination pagination)
        {
            try
            {
                _logger.LogInformation("Started GetAllProductDetails with Pagination: {Pagination}", pagination);

                _logger.LogDebug("Fetching all product details from the repository with Pagination: {@Pagination}", pagination);
                List<ProductDetails> productData = (await _productRepository.GetAllProductDetails(pagination)).ToList();
                _logger.LogDebug("Successfully fetched product details. Total products retrieved: {Count}", productData.Count);

                _logger.LogInformation("Completed GetAllProductDetails with Pagination: {Pagination}", pagination);

                return new Result { ResultObject = productData };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all product details with Pagination: {Pagination}", pagination);
                return new Result(
                    $"An error occurred while fetching all accounts, ERROR: {ex.Message}",
                    "Accounts.UnknownError",
                    HttpStatusCode.InternalServerError,
                    ex
                );
            }
        }

        private async Task<Result> ValiadationFields(SaveProductDto productModel)
        {
            try
            {
                _logger.LogInformation("Started ValiadationFields for SaveProductDto.");

                if (productModel == null)
                {
                    _logger.LogDebug("Validation failed: Product model is null.");
                    return new Result($"Product Model is null", "Products.ProductModelRequired", HttpStatusCode.BadRequest);
                }

                if (productModel.CategoryID == null)
                {
                    _logger.LogDebug("Validation failed: CategoryID is mandatory but is null.");
                    return new Result($"CategoryID is mandatory.", "Products.CategoryIDIsMandatory", HttpStatusCode.BadRequest);
                }

                if (productModel.BrandID == null)
                {
                    _logger.LogDebug("Validation failed: BrandID is mandatory but is null.");
                    return new Result($"BrandID is mandatory.", "Products.BrandIDIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validation succeeded for SaveProductDto: {@ProductModel}", productModel);
                _logger.LogInformation("Completed ValiadationFields for SaveProductDto.");
                return new Result();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during validation in ValiadationFields.");
                return new Result($"{ex.Message}", "ValidationError", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> SaveProduct(SaveProductDto productModel)
        {
            try
            {
                _logger.LogInformation("Started SaveProduct for ProductModel: {@ProductModel}", productModel);

                Products? product = null;

                #region API Validations
                _logger.LogDebug("Validating fields for SaveProduct.");
                Result validationResult = await ValiadationFields(productModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    _logger.LogDebug("Validation failed: {ValidationResult}", validationResult);
                    return validationResult;
                }
                #endregion

                if (productModel.ID == 0)
                {
                    _logger.LogDebug("Checking if product already exists with CategoryID: {CategoryID}, BrandID: {BrandID}", productModel.CategoryID, productModel.BrandID);
                    bool isProductExist = await CheckIfProductExistsAsync(productModel.CategoryID, productModel.BrandID);
                    if (isProductExist)
                    {
                        _logger.LogDebug("Product already exists with CategoryID: {CategoryID}, BrandID: {BrandID}", productModel.CategoryID, productModel.BrandID);
                        return new Result("Product is already exists", "Products.ProductIsExists", HttpStatusCode.BadRequest);
                    }
                }

                if (productModel.ID > 0)
                {
                    _logger.LogDebug("Fetching product details for ID: {ID}", productModel.ID);
                    product = await _productRepository.GetProductDetailByID(productModel.ID);

                    if (product == null)
                    {
                        _logger.LogDebug("Product not found for ID: {ID}", productModel.ID);
                        return new Result("Product not exists", "Products.ProductNotExists", HttpStatusCode.BadRequest);
                    }

                    if (productModel.Quantity <= product.UseQuantity)
                    {
                        _logger.LogDebug("Quantity ({Quantity}) is less than or equal to used quantity ({UseQuantity}) for Product ID: {ID}", productModel.Quantity, product.UseQuantity, productModel.ID);
                        return new Result("Quantity should not be less than used Quantity", "Products.ProductQuantityIsLessUseQuantity", HttpStatusCode.BadRequest);
                    }

                    product.ModifiedBy = productModel.ModifiedBy;
                }
                else
                {
                    _logger.LogDebug("Creating a new product entry.");
                    product = new Products
                    {
                        CreatedBy = productModel.CreatedBy,
                        IsDeleted = false
                    };
                }

                product.Quantity = productModel.Quantity;
                product.CategoryID = productModel.CategoryID;
                product.BrandID = productModel.BrandID;

                _logger.LogDebug("Saving product details: {@Product}", product);
                product = await _productRepository.SaveProduct(product);

                _logger.LogDebug("Executing updates for available quantity and use quantity.");
                await ExecuteUpdateAvailableQuantityAndUseQuantity();

                _logger.LogDebug("Executing updates for product codes.");
                await ExecuteUpdateProductCodes();

                _logger.LogDebug("Fetching updated product details for ID: {ID}", product.ID);
                product = await _productRepository.GetProductDetailByID(product.ID);

                _logger.LogInformation("Completed SaveProduct for ProductModel: {@ProductModel}", productModel);
                return new Result { ResultObject = product };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving product details for ProductModel: {@ProductModel}", productModel);
                return new Result("An error occurred while saving product detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<bool> CheckIfProductExistsAsync(int categoryId, int brandId)
        {
            try
            {
                _logger.LogInformation("Started CheckIfProductExistsAsync with CategoryID: {CategoryId}, BrandID: {BrandId}", categoryId, brandId);

                _logger.LogDebug("Started IsProductExistAsync for CategoryID: {CategoryId}, BrandID: {BrandId}", categoryId, brandId);
                bool isProductExist = await _productRepository.IsProductExistAsync(categoryId, brandId);
                _logger.LogDebug("Completed IsProductExistAsync ,isProductExist: {isProductExist}", isProductExist);

                _logger.LogDebug("Repository method returned: {IsProductExist} for CategoryID: {CategoryId}, BrandID: {BrandId}", isProductExist, categoryId, brandId);
                _logger.LogInformation("Completed CheckIfProductExistsAsync with CategoryID: {CategoryId}, BrandID: {BrandId}", categoryId, brandId);

                return isProductExist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CheckIfProductExistsAsync with CategoryID: {CategoryId}, BrandID: {BrandId}", categoryId, brandId);
                throw; // Re-throw the exception to preserve stack trace for higher-level handling.
            }
        }

        public async Task<bool> ExecuteUpdateProductCodes()
        {
            try
            {
                _logger.LogInformation("Started ExecuteUpdateProductCodes.");

                _logger.LogDebug("Started repository method to update product codes.");
                await _productRepository.ExecuteUpdateProductCodes();
                _logger.LogDebug("Product codes update completed successfully.");

                _logger.LogInformation("Completed ExecuteUpdateProductCodes.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during ExecuteUpdateProductCodes.");
                return false;
            }
        }

        public async Task<Result> GetUserListByProductID(int id)
        {
            try
            {
                _logger.LogInformation("Started GetUserListByProductID with ProductID: {ProductID}", id);

                _logger.LogDebug("Fetching user list by ProductID: {ProductID}", id);
                List<UserByProductID> userList = (await _productRepository.GetUserListByProductID(id)).ToList();
                _logger.LogDebug("Successfully fetched user list. Total users retrieved: {Count}", userList.Count);

                _logger.LogInformation("Completed GetUserListByProductID for ProductID: {ProductID}", id);

                return new Result { ResultObject = userList };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user list by ProductID: {ProductID}", id);
                return new Result("An error occurred while fetching user list by product id, ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> DeleteProduct(DeleteProductDto productModel)
        {
            try
            {
                _logger.LogInformation("Started DeleteProduct for ProductModel: {@ProductModel}", productModel);

                if (productModel == null)
                {
                    _logger.LogDebug("Validation failed: ProductModel is null.");
                    return new Result($"Product Model is null.", "Products.ProductModelIsNull", HttpStatusCode.BadRequest);
                }

                if (productModel.Id == null)
                {
                    _logger.LogDebug("Validation failed: ProductId is mandatory but is null.");
                    return new Result($"ProductId is mandatory.", "Products.ProductIdIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Fetching product details for ProductId: {ProductId}", productModel.Id);
                Products? product = await _productRepository.GetProductDetailByID(productModel.Id);

                if (product == null)
                {
                    _logger.LogDebug("Product not found for ProductId: {ProductId}", productModel.Id);
                    return new Result("Product not exists", "Products.ProductNotExists", HttpStatusCode.BadRequest);
                }

                if (product.UseQuantity >= 1)
                {
                    _logger.LogDebug("Product with ProductId: {ProductId} is assigned to a user.", productModel.Id);
                    return new Result("Product is already assigned to user.", "Products.ProductIsAssignedToUser", HttpStatusCode.BadRequest);
                }

                product.DeletedBy = productModel.DeletedBy;
                _logger.LogDebug("Deleting product with ProductId: {ProductId}", productModel.Id);
                product = await _productRepository.DeleteProduct(product);

                _logger.LogDebug("Executing updates for available quantity and use quantity.");
                await ExecuteUpdateAvailableQuantityAndUseQuantity();

                _logger.LogDebug("Executing updates for product codes.");
                await ExecuteUpdateProductCodes();

                _logger.LogInformation("Completed DeleteProduct for ProductId: {ProductId}", productModel.Id);
                return new Result { ResultObject = product };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product for ProductModel: {@ProductModel}", productModel);
                return new Result("An error occurred while deleting product", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<List<int>> GetProductIdsByCategoryAndBrandAsync(int categoryId, int brandId)
        {
            try
            {
                _logger.LogInformation("Started GetProductIdsByCategoryAndBrandAsync with CategoryId: {CategoryId}, BrandId: {BrandId}", categoryId, brandId);

                _logger.LogDebug("Calling repository method to fetch product IDs for CategoryId: {CategoryId}, BrandId: {BrandId}", categoryId, brandId);
                List<int> productIds = await _productRepository.GetProductIdsByCategoryAndBrandAsync(categoryId, brandId);
                _logger.LogDebug("Successfully fetched {ProductIdsCount} product IDs for CategoryId: {CategoryId}, BrandId: {BrandId}", productIds.Count, categoryId, brandId);

                _logger.LogInformation("Completed GetProductIdsByCategoryAndBrandAsync with CategoryId: {CategoryId}, BrandId: {BrandId}", categoryId, brandId);
                return productIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching product IDs for CategoryId: {CategoryId}, BrandId: {BrandId}", categoryId, brandId);
                throw; // Re-throw the exception to preserve stack trace for higher-level handling.
            }
        }

    }

}
