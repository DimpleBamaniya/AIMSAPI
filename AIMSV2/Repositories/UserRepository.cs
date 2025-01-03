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
        public async Task<Users> GetUserDetailByID(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ID == id && !u.IsDeleted);
        }

        public async Task<Entities.Users> SaveUser(Entities.Users account)
        {
            DateTime utcNow = DateTime.UtcNow;
            DateOnly currentUtcDate = DateOnly.FromDateTime(utcNow);
            if (account.ID == 0)
            {
                account.Created = currentUtcDate;
                _context.Users.Add(account);
            }
            else
            {
                account.Modified = currentUtcDate;
                _context.Users.Update(account);
            }

            await _context.SaveChangesAsync();
            return account;
        }
    }
}
