namespace Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteUpdateUserPermissions()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateUserPermissions]");
    }
    public async Task ExecuteUpdateUserCodes()
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[usp_UpdateUserCodes]");
    }
    public async Task<Users> GetUserDetailByEmailIDAsync(string emailID)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailID == emailID && !u.IsDeleted);
    }
    public async Task<Users> GetUserDetailByIDAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.ID == id && !u.IsDeleted);
    }

    public async Task<Users> SaveUserAsync(Users user)
    {
        DateTime utcNow = DateTime.UtcNow;
        if (user.ID == 0)
        {
            user.Created = utcNow;
            _context.Users.Add(user);
        }
        else
        {
            user.Modified = utcNow;
            _context.Users.Update(user);
        }

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<UserDetailsList>> GetAllUserDetailsAsync(Pagination pagination)
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

    public async Task<Users> DeleteUserAsync(Users user)
    {
        DateTime utcNow = DateTime.UtcNow;
        user.Deleted = utcNow;
        user.IsDeleted = true;
            _context.Users.Update(user);

        await _context.SaveChangesAsync();
        return user;
    }
} 
