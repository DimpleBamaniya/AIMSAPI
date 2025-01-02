using System;
using System.Data;
using AIMSV2.Data;
using AIMSV2.Models;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AIMSV2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Entities.Users>> GetAllUsers()
        {
             return await _context.Users.ToListAsync();
        }

        public async Task ExecuteUpdateUserPermissions()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp.UpdateUserPermissions]");
        }
        public async Task ExecuteUpdateUseQuantity()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp.UpdateUseQuantity]");
        }
        public async Task ExecuteUpdateAvailableQuantity()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp.UpdateAvailableQuantity]");
        }
        public async Task ExecuteUpdateAvailableQuantityAndUseQuantity()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp.UpdateAvailableQuantityAndUseQuantity]");
        }
        public async Task<Users> GetUserDetailByEmailID(string emailID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailID == emailID && !u.IsDeleted);
        }
    }
}
