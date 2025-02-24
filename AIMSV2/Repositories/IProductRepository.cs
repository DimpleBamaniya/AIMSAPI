namespace Repositories;
public interface IProductRepository
{
    Task ExecuteUpdateUseQuantity();
    Task ExecuteUpdateAvailableQuantity();
    Task ExecuteUpdateAvailableQuantityAndUseQuantity();
    Task<Products> GetProductDetailByID(int id);
    Task<List<ProductDetails>> GetAllProductDetailsAsync(Pagination pagination);
    Task<Products> SaveProductAsync(Products product);
    Task<bool> IsProductExistAsync(int categoryId,int brandId);
    Task ExecuteUpdateProductCodes();
    Task<List<UserByProductID>> GetUserListByProductIDAsync(int productId);
    Task<List<int>> GetProductIdsByCategoryAndBrandAsync(int categoryId, int brandId);
    Task<Products> DeleteProductAsync(Products product);


}
