namespace Services;
public interface IProductService
{
    Task<bool> ExecuteUpdateUseQuantity();
    Task<bool> ExecuteUpdateAvailableQuantity();
    Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity();
    Task<Result> GetProductDetailByID(int id);
    Task<Result> GetAllProductDetailsAsync(Pagination pagination);
    Task<Result> SaveProductAsync(SaveProductDto productModel);
    Task<bool> CheckIfProductExistsAsync(int categoryId,int brandId);
    Task<bool> ExecuteUpdateProductCodes();
    Task<Result> GetUserListByProductIDAsync(int id);
    Task<Result> DeleteProductAsync(DeleteProductDto productModel);
    Task<List<int>> GetProductIdsByCategoryAndBrandAsync(int categoryId, int brandId);
}
