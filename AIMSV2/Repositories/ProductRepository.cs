namespace Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteUpdateUseQuantity()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateUseQuantity]");
    }

    public async Task ExecuteUpdateAvailableQuantity()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateAvailableQuantity]");
    }

    public async Task ExecuteUpdateAvailableQuantityAndUseQuantity()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateAvailableQuantityAndUseQuantity]");
    }

    public async Task<Products> GetProductDetailByID(int id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id && !u.IsDeleted);
    }

    public async Task<List<ProductDetails>> GetAllProductDetailsAsync(Pagination pagination)
    {
        return await _context.usp_GetProductsWithPagination
            .FromSqlRaw("Exec usp_GetProductsWithPagination @SearchString={0},@PageNo={1}, @PageSize={2}, @SortColumn={3}, @SortOrder={4}",
                pagination.SearchString ?? string.Empty,
                pagination.PageNo,
                pagination.PageSize,
                pagination.SortColumn ?? string.Empty,
                pagination.SortOrder ?? string.Empty)
            .ToListAsync();
    }
    public async Task<Products> SaveProductAsync(Products product)
    {
        DateTime utcNow = DateTime.UtcNow;
        if (product.ID == 0)
        {
            product.Created = utcNow;
            _context.Products.Add(product);
        }
        else
        {
            product.Modified = utcNow;
            _context.Products.Update(product);
        }

        await _context.SaveChangesAsync();
        return product;
    }


    public async Task<bool> IsProductExistAsync(int categoryId, int brandId)
    {
        // Define parameters for the stored procedure
        var categoryIdParam = new SqlParameter("@CategoryID", categoryId);
        var brandIdParam = new SqlParameter("@BrandID", brandId);

        // Execute the stored procedure using FromSqlRaw and map the result to the SqlResult class
        var result = await _context.usp_IsExistProduct
            .FromSqlRaw("EXEC [dbo].[usp_IsExistProduct] @BrandID = {0}, @CategoryID = {1}", brandId, categoryId)
            .ToListAsync();

        // Return the first result's boolean value if present
        return result.Count > 0 && result[0].Result;
    }

    public async Task ExecuteUpdateProductCodes()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateProductCodes]");
    }

    public async Task<List<UserByProductID>> GetUserListByProductIDAsync(int id)
    {
        return await _context.usp_getUserListbyProductID
            .FromSqlRaw("Exec usp_getUserListbyProductID @ProductID={0}", id)
            .ToListAsync();
    }

    public async Task<Products> DeleteProductAsync(Products product)
    {
        DateTime utcNow = DateTime.UtcNow;
        if (product.ID != 0 || product.ID != null)
        {
            product.Deleted = utcNow;
            product.IsDeleted = true;
            _context.Products.Update(product);
        }

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<int>> GetProductIdsByCategoryAndBrandAsync(int categoryId, int brandId)
    {
        // Query the view and filter by CategoryID and BrandID
        return await _context.Products.AsNoTracking()
                             .Where(p => p.CategoryID == categoryId && p.BrandID == brandId && p.IsDeleted == false)
                             .Select(p => p.ID)
                             .ToListAsync();
    }


}