using AIMSV2.Data;
using AIMSV2.Models;
using AIMSV2.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSAPI.Repositories
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserProducts>> GetAllUserProduct()
        {
            return await _context.UserProducts.ToListAsync();
        }
        public async Task<List<UserProducts>> GetAllUserProductAsync()
        {
            return await _context.UserProducts.ToListAsync();
        }
        public async Task<List<ProductByUserID>> GetProductListbyUserID(int id)
        {
            return await _context.usp_getProductListbyUserID
                .FromSqlRaw("Exec usp_getProductListbyUserID @UserID={0}",id)
                .ToListAsync();
        }
    }
}