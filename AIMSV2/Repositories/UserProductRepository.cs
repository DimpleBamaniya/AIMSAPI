using AIMSV2.Data;
using AIMSV2.Entities;
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
        //remove method
        public async Task<IEnumerable<UserProducts>> GetAllUserProduct()
        {
            return await _context.UserProducts.ToListAsync();
        }
        public async Task<List<UserProducts>> GetAllUserProductAsync()
        {
            return await _context.vw_UserProducts.ToListAsync();
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

        public async Task<UserProducts> SaveUserProducts(UserProducts userProducts)
        {
            DateTime utcNow = DateTime.UtcNow;
            DateOnly currentUtcDate = DateOnly.FromDateTime(utcNow);
            if (userProducts.ID == 0)
            {
                userProducts.AssignedDate = currentUtcDate;
                _context.UserProducts.Add(userProducts);
            }
            await _context.SaveChangesAsync();
            return userProducts;
        }

        public async Task<List<ProductByUserID>> GetProductListbyUserID(int id)
        {
            return await _context.usp_GetProductListbyUserID
                .FromSqlRaw("Exec usp_GetProductListbyUserID @UserID={0}", id)
                .ToListAsync();
        }
    }
}