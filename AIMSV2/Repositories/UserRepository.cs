﻿using System;
using System.Data;
using AIMSV2.Data;
using AIMSV2.Entities;
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
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
             return await _context.Users.ToListAsync();
        }

        public async Task ExecuteUpdateUserPermissions()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateUserPermissions]");
        }
        public async Task ExecuteUpdateUserCodes()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateUserCodes]");
        }
        public async Task<Users> GetUserDetailByEmailID(string emailID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailID == emailID && !u.IsDeleted);
        }
        public async Task<Users> GetUserDetailByID(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ID == id && !u.IsDeleted);
        }

        public async Task<Users> SaveUser(Users user)
        {
            DateTime utcNow = DateTime.UtcNow;
            DateOnly currentUtcDate = DateOnly.FromDateTime(utcNow);
            if (user.ID == 0)
            {
                user.Created = currentUtcDate;
                _context.Users.Add(user);
            }
            else
            {
                user.Modified = currentUtcDate;
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<UserDetailsList>> GetAllUserDetails()
        {
            return await _context.vw_Users.ToListAsync();
        }

        public async Task<List<UserDetailsList>> GetAllUserDetails(Pagination pagination)
        {
            return await _context.usp_GetUsersWithPagination
                .FromSqlRaw("Exec usp_GetUsersWithPagination @SearchString={0},@PageNo={1}, @PageSize={2}, @SortColumn={3}, @SortOrder={4}",
                    pagination.SearchString ?? string.Empty,
                    pagination.PageNo,
                    pagination.PageSize,
                    pagination.SortColumn ?? string.Empty,
                    pagination.SortOrder ?? string.Empty)
                .ToListAsync();
        }
    } 
}
