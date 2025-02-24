namespace Repositories;
public interface IUserRepository
{
    Task<Users> GetUserDetailByIDAsync(int id);
    Task<Users> SaveUserAsync(Users user);
    Task<Users> GetUserDetailByEmailIDAsync(string emailID);
    Task ExecuteUpdateUserPermissions();
    Task ExecuteUpdateUserCodes();
    Task<List<UserDetailsList>> GetAllUserDetailsAsync(Pagination pagination);
    Task<Users> DeleteUserAsync(Users user);

}
