using AIMSV2.Data;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
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
            return await _context.Products.FirstOrDefaultAsync(u => u.ID == id && !u.IsDeleted);
        }

        public async Task<List<ProductDetails>> GetAllProductDetails(Pagination pagination)
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

    }
}