namespace Repositories;
public class UserProductRepository : IUserProductRepository
{
    private readonly ApplicationDbContext _context;

    public UserProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
  
    public async Task<List<UserProducts>> GetAllUserProductAsync()
    {
        return await _context.vw_UserProducts.AsNoTracking().ToListAsync();
    }

    public async Task<bool> DeleteUserProductAsync(int id)
    {
        // Retrieve the entity
        var userProduct = await _context.UserProducts.FindAsync(id);
        if (userProduct == null)
        {
            return false; // Not found
        }

        // Remove the entity
        _context.UserProducts.Remove(userProduct);

        // Save changes
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<UserProducts> SaveUserProductsAsync(UserProducts userProducts)
    {
        DateTime utcNow = DateTime.UtcNow;
        if (userProducts.ID == 0)
        {
            userProducts.AssignedDate = utcNow;
            _context.UserProducts.Add(userProducts);
        }
        await _context.SaveChangesAsync();
        return userProducts;
    }

    public async Task<List<ProductByUserID>> GetProductListbyUserIDAsync(int id)
    {
        return await _context.usp_GetProductListbyUserID
            .FromSqlRaw("Exec usp_GetProductListbyUserID @UserID={0}", id)
            .ToListAsync();
    }

    public async Task<CheckUserProductCategoryMatchDto> CheckUserProductCategoryMatchAsync(int userId, int categoryId)
    {
        // Execute the stored procedure and return the result
        var result = await _context.usp_CheckUserProductCategoryMatch
            .FromSqlRaw("EXEC usp_CheckUserProductCategoryMatch @UserID = {0}, @CategoryID = {1}", userId, categoryId)
            .ToListAsync(); // Ensure that the result is materialized

        // If the result is not empty, return the first item, otherwise return null
        return result.FirstOrDefault();
    }
}