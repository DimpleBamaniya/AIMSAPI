using System.Net;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;

namespace AIMSV2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteUpdateUseQuantity()
        {
            try
            {
                await _productRepository.ExecuteUpdateUseQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantity()
        {
            try
            {
                await _productRepository.ExecuteUpdateAvailableQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity()
        {
            try
            {
                await _productRepository.ExecuteUpdateAvailableQuantityAndUseQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<Result> GetProductDetailByID(int id)
        {
            try
            {
                #region API Validations
                Result validationResult = await ValiadationFieldsToGetProductDetailByID(id);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return validationResult;
                }
                #endregion

                Products product = await _productRepository.GetProductDetailByID(id);
                if (product == null)
                {
                    return new Result($"Product is not Exists.", "Product.ProductNotExists", HttpStatusCode.BadRequest);
                }
                else
                {
                    return new Result { ResultObject = product };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }

        }

        private async Task<Result> ValiadationFieldsToGetProductDetailByID(int id)
        {
            if (id == null)
            {
                return new Result($"ID is null", "Accounts.IDIsRequired", HttpStatusCode.BadRequest);
            }
            return new Result();
        }

        public async Task<Result> GetAllProductDetails(Pagination pagination)
        {
            try
            {
                List<ProductDetails> productData = (await _productRepository.GetAllProductDetails(pagination)).ToList();
                return new Result { ResultObject = productData };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task<Result> ValiadationFields(SaveProductDto productModel)
        {
            if (productModel == null)
            {
                return new Result($"Product Model is null", "Products.ProductModelRequired", HttpStatusCode.BadRequest);
            }
            if (productModel.CategoryID == null)
            {
                return new Result($"CategoryID is mandatory.", "Products.CategoryIDIsMandatory", HttpStatusCode.BadRequest);
            }
            if (productModel.BrandID == null)
            {
                return new Result($"BrandID is mandatory.", "Products.BrandIDIsMandatory", HttpStatusCode.BadRequest);
            }

            return new Result();
        }

        public async Task<Result> SaveProduct(SaveProductDto productModel)
        {
            try
            {

                Products? product = null;
                #region API Validations
                Result validationResult = await ValiadationFields(productModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return validationResult;
                }
                #endregion
                if (productModel.ID == 0)
                {
                    bool isProductExist = await CheckIfProductExistsAsync(productModel.CategoryID, productModel.BrandID);
                    if (isProductExist)
                    {
                        return new Result("product is already exists", "products.productIsExists", HttpStatusCode.BadRequest);
                    }
                }

                if (productModel.ID > 0)
                {
                    product = await _productRepository.GetProductDetailByID(productModel.ID);

                    if (product == null)
                    {
                        return new Result("product not exists", "products.productNotExists", HttpStatusCode.BadRequest);
                    }

                    if (productModel.Quantity < product.UseQuantity)
                    {
                        return new Result("Quantity should not less than used Quantity", "products.productQuantityIsLessUseQuantity", HttpStatusCode.BadRequest);
                    }
                    product.ModifiedBy = productModel.ModifiedBy;
                }
                else
                {
                    product = new Products
                    {
                        CreatedBy = productModel.CreatedBy,
                        IsDeleted = false
                    };
                }
                product.Quantity = productModel.Quantity;
                product.CategoryID = productModel.CategoryID;
                product.BrandID = productModel.BrandID;
                product = await _productRepository.SaveProduct(product);
                await ExecuteUpdateAvailableQuantityAndUseQuantity();
                await ExecuteUpdateProductCodes();
                // letter change
                return new Result { ResultObject = product };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while save user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<bool> CheckIfProductExistsAsync(int categoryId, int brandId)
        {
            // Call the repository method and return the result
            return await _productRepository.IsProductExistAsync(categoryId, brandId);
        }

        public async Task<bool> ExecuteUpdateProductCodes()
        {
            try
            {
                await _productRepository.ExecuteUpdateProductCodes();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<Result> GetUserListByProductID(int id)
        {
            try
            {
                List<UserByProductID> userList = (await _productRepository.GetUserListByProductID(id)).ToList();
                return new Result { ResultObject = userList };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while fetching all user list by product id, ERROR: " + ex.Message, "Products.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> DeleteProduct(DeleteProductDto productModel)
        {
            try
            {

                Products? product = null;
                if (productModel == null)
                {
                    return new Result($"product Model is null.", "Products.productModelIsNull", HttpStatusCode.BadRequest);
                }

                if (productModel.Id == null)
                {
                    return new Result($"productId is mandatory.", "Products.productIdIsMandatory", HttpStatusCode.BadRequest);
                }

                product = await _productRepository.GetProductDetailByID(productModel.Id);

                if (product == null)
                {
                    return new Result("product not exists", "products.productNotExists", HttpStatusCode.BadRequest);
                }

                if (product.UseQuantity >= 1)
                {
                    return new Result("Product is already assign to user.", "products.productIsAssignedToUser", HttpStatusCode.BadRequest);
                }
                product.DeletedBy = productModel.DeletedBy;

                product = await _productRepository.DeleteProduct(product);
                await ExecuteUpdateAvailableQuantityAndUseQuantity();
                await ExecuteUpdateProductCodes();
                // letter change
                return new Result { ResultObject = product };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while save user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<List<int>> GetProductIdsByCategoryAndBrandAsync(int categoryId, int brandId)
        {
            return await _productRepository.GetProductIdsByCategoryAndBrandAsync(categoryId, brandId);
        }
    }

}
